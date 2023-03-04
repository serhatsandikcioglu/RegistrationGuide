using NLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Data
{
    public  interface ICustomerRepository : IGenericRepository<Customer>
    {
        List<string> GetNamesFromNumber(string number);
    }
}
