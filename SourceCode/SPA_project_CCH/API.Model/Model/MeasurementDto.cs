using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.Model
{
    public class MeasurementDto
    {
        public DateTime date { get; set; }
        public double BMI { get; set; }
        public string colorBMI { get; set; }
        public double bodyMass { get; set; }
        public string colorBodyMass { get; set; }
        public double fatContent { get; set; }
        public string colorFatContent { get; set; }
        public double muscleContent { get; set; }
        public string colorMuscleContent { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public string colorWeight { get; set; }
    }
}
