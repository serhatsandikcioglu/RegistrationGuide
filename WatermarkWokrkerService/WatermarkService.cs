using NLayer.Service;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using NLayer.Data.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace WatermarkWokrkerService
{
    public class WatermarkService : BackgroundService
    {
        private readonly ILogger<WatermarkService> _logger;
        private readonly RabbitMQClientService _rabbitMQClientService;
        private IModel _channel;
        private IServiceProvider _serviceProvider;
        public WatermarkService(ILogger<WatermarkService> logger, RabbitMQClientService rabbitMQClientService, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _rabbitMQClientService = rabbitMQClientService;
            _serviceProvider = serviceProvider;
        }




        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMQClientService.ConnectForWatermark();
            _channel.BasicQos(0, 1, false);
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var consumer = new AsyncEventingBasicConsumer(_channel);
                _channel.BasicConsume(RabbitMQClientService.QueueName, false, consumer);
                consumer.Received += AddWatermark;
                Task.CompletedTask.Wait();
                await Task.Delay(2000*60, stoppingToken);
            }
        }

        private async Task<Task> AddWatermark(object sender,BasicDeliverEventArgs @event)
        {
            try
            {
                var customer = JsonSerializer.Deserialize<Customer>(Encoding.UTF8.GetString(@event.Body.ToArray()));
                using (MemoryStream ms = new MemoryStream(customer.Photography))
                {
                    using (Image image = Image.FromStream(ms))
                    {
                        using (Graphics graphics = Graphics.FromImage(image))
                        {
                            Font watermarkFont = new Font("Arial", 100, FontStyle.Regular);
                            Brush watermarkBrush = new SolidBrush(Color.Red);
                            PointF watermarkLocation = new PointF(20, 20); // Filigranın konumu
                            graphics.DrawString("www.mysite.com", watermarkFont, watermarkBrush, watermarkLocation);
                        }

                        using (MemoryStream msOutput = new MemoryStream())
                        {
                            image.Save(msOutput, ImageFormat.Jpeg);
                            var imagebytewithwatermark = msOutput.ToArray();

                            using (var scope = _serviceProvider.CreateScope())
                            {
                                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                                context.Customers.FirstOrDefault(x => x.Id == customer.Id).Photography = imagebytewithwatermark;
                                context.SaveChanges();

                            }
                        }
                    }
                }
              

                _channel.BasicAck(@event.DeliveryTag, false);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
            }
            return Task.CompletedTask;
        }
    }
}

