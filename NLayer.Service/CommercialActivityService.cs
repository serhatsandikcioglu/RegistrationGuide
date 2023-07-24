using NLayer.Data;
using NLayer.Data.Models;
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

        public CommercialActivityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(CommercialActivity commercialactivity)
        {
            _unitOfWork.CommercialActivityRepository.Add(commercialactivity);
            _unitOfWork.Commit();
        }

        public void Delete(int commercialactivityid)
        {
            _unitOfWork.CommercialActivityRepository.Delete(commercialactivityid);
            _unitOfWork.Commit();
        }

        public List<CommercialActivity> GetAll()
        {
            return _unitOfWork.CommercialActivityRepository.GetAll();
        }

        public CommercialActivity GetById(int commercialactivityid)
        {
            return _unitOfWork.CommercialActivityRepository.GetById(commercialactivityid);
        }

        public void Update(CommercialActivity commercialactivity)
        {
            _unitOfWork.CommercialActivityRepository.Update(commercialactivity);
            _unitOfWork.Commit();
        }
    }
}
