using Microsoft.EntityFrameworkCore;
using NLayer.Data.Models;
using NLayer.Service;
using RabbitMQ.Client;
using WatermarkWokrkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context,services) =>
    {

        IConfiguration Configuration = context.Configuration;
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(Configuration.GetConnectionString("SqlCon"));
        });
        services.AddSingleton<RabbitMQClientService>();
        services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(Configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true });
        services.AddHostedService<Worker>();
        services.AddHostedService<WatermarkService>();

    })
    .Build();

await host.RunAsync();
