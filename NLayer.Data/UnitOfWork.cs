using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NLayer.Data.Models;

namespace NLayer.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(AppDbContext appDbContext, IServiceProvider serviceProvider)
        {
            _appDbContext = appDbContext;
            _serviceProvider = serviceProvider;
        }
        private ICustomerRepository _customerRepository;
        private ICommercialActivityRepository _commercialActivityRepository;

        public ICustomerRepository CustomerRepository
        {
            get
            {
                if (_customerRepository == default(ICustomerRepository))
                    _customerRepository = _serviceProvider.GetRequiredService<ICustomerRepository>();
                return _customerRepository;
            }

        }
        public ICommercialActivityRepository CommercialActivityRepository
        {
            get
            {
                if (_commercialActivityRepository == default(ICommercialActivityRepository))
                    _commercialActivityRepository = _serviceProvider.GetRequiredService<ICommercialActivityRepository>();
                return _commercialActivityRepository;
            }

        }

        public void Commit()
        {
            _appDbContext.SaveChanges();
        }
    }
}
