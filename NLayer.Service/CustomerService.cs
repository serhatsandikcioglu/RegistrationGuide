using Azure.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using NLayer.Data;
using NLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service
{
    public class CustomerService : ICustomerService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Customer> _customerRepository;

        public CustomerService(IUnitOfWork unitOfWork, IGenericRepository<Customer> genericRepository)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = genericRepository;
        }

        public void Create(Customer customer)
        {
            _customerRepository.Add(customer);
            _unitOfWork.Commit();
        }

        public void Delete(int customerId)
        {
            _customerRepository.Delete(customerId);
            _unitOfWork.Commit();
        }

        public List<Customer> GetAll()
        {
            return _customerRepository.GetAll();
        }

        public Customer GetById(int customerId)
        {
            return _customerRepository.GetById(customerId);
        }

        public void Update(Customer customer)
        {
            _customerRepository.Update(customer);
            _unitOfWork.Commit();
        }

        public string GetNamesFromNumber(string number)
        {
            string Succes = "This number does not have more than one name";
            string failed = "This number has more than one name";
            string null1 = "This number Does not exist";
            var ad = _customerRepository.GetAll().Where(x => x.PhoneNumber == number).Select(x => x.Name).ToList();
            if (ad.Count == 1)
            {
                return (Succes);
            }
            if (ad.Count > 1)
            {
                return (failed);
            }

            return (null1);
        }
    }
}
