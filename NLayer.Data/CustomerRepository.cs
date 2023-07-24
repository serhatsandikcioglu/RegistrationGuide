using Microsoft.EntityFrameworkCore;
using NLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Data
{
    public class CustomerRepository : GenericRepository<Customer>,  ICustomerRepository
    {
         private readonly DbSet<Customer> _dbSet;

        public CustomerRepository(AppDbContext appDbContext) : base (appDbContext)
        {
            _dbSet = appDbContext.Set<Customer>();
        }
         public List<string> GetNamesFromNumber(string number)
         {
             return _dbSet.Where(x => x.PhoneNumber == number).Select(x => x.Name).ToList();
         }
    }

}
