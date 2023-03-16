using NLayer.Data;
using NLayer.Data.Models;
using NLayer.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service
{
    public class CommercialActivityService : ICommercialActivityService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<CommercialActivity> _commercialActivityRepository;

        public CommercialActivityService(IUnitOfWork unitOfWork, IGenericRepository<CommercialActivity> commercialActivityRepository)
        {
            _unitOfWork = unitOfWork;
            _commercialActivityRepository = commercialActivityRepository;
        }

        public void Add(CommercialActivity commercialactivity)
        {
            _commercialActivityRepository.Add(commercialactivity);
            _unitOfWork.Commit();
        }

        public void Delete(int commercialactivityid)
        {
            _commercialActivityRepository.Delete(commercialactivityid);
            _unitOfWork.Commit();
        }

        public List<CommercialActivity> GetAll()
        {
            return _commercialActivityRepository.GetAll();
        }

        public CommercialActivity GetById(int commercialactivityid)
        {
            return _commercialActivityRepository.GetById(commercialactivityid);
        }

        public void Update(CommercialActivity commercialactivity)
        {
            _commercialActivityRepository.Update(commercialactivity);
            _unitOfWork.Commit();
        }
    }
}
