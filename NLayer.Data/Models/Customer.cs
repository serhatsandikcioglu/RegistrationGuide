using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Data.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public byte[]? Photography { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }

        public List<CommercialActivity> CommercialActivities { get; set; }
    }
}
