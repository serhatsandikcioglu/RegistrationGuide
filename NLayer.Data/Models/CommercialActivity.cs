using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Data.Models
{
    public class CommercialActivity
    {
        public int Id { get; set; }

        public Customer Customer { get; set; }
        public string Service { get; set; }
        public int Price { get; set; }
        public DateTime Date { get; set; }

    }
}
