using Azure.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using NLayer.Data;
using NLayer.Data.Models;
using NLayer.Service.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace NLayer.Service
{
    public class CustomerService : ICustomerService
    {
        private IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        private readonly RabbitMQPublisher _rabbitMQPublisher;

        public CustomerService(IUnitOfWork unitOfWork, ICustomerRepository customerRepository, RabbitMQPublisher rabbitMQPublisher)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        public void Add(Customer customer)
        {
            _customerRepository.Add(customer);
            var imagebyte = customer.Photography;
            customer.Photography = null;
            _unitOfWork.CustomerRepository.Add(customer);
            _unitOfWork.Commit();
            _rabbitMQPublisher.PublishForWatermark(new Customer() {Id=customer.Id,  Photography=imagebyte.ToArray()});
        }

        public void Delete(int customerId)
        {
            _unitOfWork.CustomerRepository.Delete(customerId);
            _unitOfWork.Commit();
        }

        public List<Customer> GetAll()
        {
            return _unitOfWork.CustomerRepository.GetAll();
        }

        public Customer GetById(int customerId)
        {
            return _unitOfWork.CustomerRepository.GetById(customerId);
        }

        public void Update(Customer customer)
        {
            _unitOfWork.CustomerRepository.Update(customer);
            _unitOfWork.Commit();
        }
        public string GetNamesFromNumber(string number)
        {
            string Succes = "This number does not have more than one name";
            string failed = "This number has more than one name";
            string null1 = "This number does not exist";
            var names = _unitOfWork.CustomerRepository.GetNamesFromNumber(number);
            if (names.Count == 1)
            {
                return (Succes);
            }
            if (names.Count > 1)
            {
                return (failed);
            }

            return (null1);
        }


    }
}

    

