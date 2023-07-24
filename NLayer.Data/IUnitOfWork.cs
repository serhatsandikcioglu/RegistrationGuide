using NLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Data
{
    public interface IUnitOfWork
    {
        void Commit();
        //IGenericRepository<Customer> CustomerRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        ICommercialActivityRepository CommercialActivityRepository { get; }
    }
}
