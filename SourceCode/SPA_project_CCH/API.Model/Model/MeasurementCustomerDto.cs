using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.Model
{
    public class MeasurementCustomerDto
    {
        public int IDBooking { get; set; }
        public string SessionDay { get; set; }
        public string Outletname { get; set; }
        public string TherapistName { get; set; }
        public byte[] Image1 { get; set; }
        public byte[] Image2 { get; set; }
        public byte[] ImageBefore { get; set; }

    }
}
