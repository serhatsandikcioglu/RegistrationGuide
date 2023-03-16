using NLayer.Data;
using NLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Interface
{
    public interface ICustomerService
    {
        List<Customer> GetAll();
        void Delete(int customerId);
        void Update(Customer customer);
        void Add(Customer customer);
        Customer GetById(int customerId);
        string GetNamesFromNumber(string number);
    }
}
