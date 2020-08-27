using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// NEW
namespace API.Model.Model
{
    public class ServiceDetail
    {
        public int ID { get; set; }
        public byte[] Image { get; set; }
        public string Name { get; set; }

       // Xem sau
       // public string numOfTimeSlot { get; set; }

        public double Price { get; set; }
        public int Star { get; set; }
        public int serviceType { get; set; }
    }
}
