using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.Model
{
   public class AppointmentDetailDto
    {
        public int Appointment { get; set; }
        public int Service { get; set; }
        //public int Customer { get; set; }
        public string feedBack { get; set; }
        public DateTime Date { get; set; }
    }

}
