using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.Model
{
    public class ServiceDTO
    {
        public byte[] Image { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        // buffer time public
        public int NumOfTimeSlot { get; set; }

        public int Star { get; set; }

        public int ID { get; set; }

    }
}
