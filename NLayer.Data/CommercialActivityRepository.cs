using Microsoft.EntityFrameworkCore;
using NLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Data
{
    public class CommercialActivityRepository : GenericRepository<CommercialActivity> , ICommercialActivityRepository
    {
        private readonly DbSet<Customer> _dbSet;

        public CommercialActivityRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _dbSet = appDbContext.Set<Customer>();
        }

    }
}
