using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.Model
{
    public class BodyMeasurementCustomerDTO
    {
        public int Height { get; set; }
        public int Weight { get; set; }
        public int BMI { get; set; }
        public int Bodymass { get; set; }
        public int Fatcontent { get; set; }
        public int Musclecontent { get; set; }
        public int Stomachfat { get; set; }
    }
}
