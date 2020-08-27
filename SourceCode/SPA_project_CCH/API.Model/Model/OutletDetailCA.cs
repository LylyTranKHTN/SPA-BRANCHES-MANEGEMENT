using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.Model
{
    public class OutletDetailCA
    {
        public int ID { get; set; }
        public string address { get; set; }
        public byte[] Image { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public float Star { get; set; }
        public int Review { get; set; }
    }
}
