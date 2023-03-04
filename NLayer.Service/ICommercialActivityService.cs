using NLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service
{
    public interface ICommercialActivityService
    {
        List<CommercialActivity> GetAll();
        void Delete(int commercialactivityid);
        void Update(CommercialActivity commercialactivity);
        void Add(CommercialActivity commercialactivity);
        CommercialActivity GetById(int commercialactivityid);
    }
}
