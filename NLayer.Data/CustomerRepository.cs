using Microsoft.EntityFrameworkCore;
using NLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Data
{
    public class CustomerRepository : ICustomerRepository, IGenericRepository<Customer>
    {
         private readonly DbSet<Customer> _dbSet;

        public CustomerRepository(AppDbContext appDbContext)
        {
            _dbSet = appDbContext.Set<Customer>();
        }

        public void Add(Customer entity)
         {
             _dbSet.Add(entity);
         }

         public void Delete(int id)
         {
             _dbSet.Remove(GetById(id));
         }

         public List<Customer> GetAll()
         {
             return _dbSet.ToList();
         }

         public Customer GetById(int id)
         {
             return _dbSet.Find(id);
         }

         public List<string> GetNamesFromNumber(string number)
         {
             return _dbSet.Where(x => x.PhoneNumber == number).Select(x => x.Name).ToList();
         }

         public void Update(Customer entity)
         {
             _dbSet.Update(entity);
         }
    }

}
