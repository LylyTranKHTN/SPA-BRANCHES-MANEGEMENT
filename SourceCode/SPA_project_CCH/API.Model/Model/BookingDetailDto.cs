using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.Model
{
   

    public class ServiceAndTherapsitDto
    {
        public string nameService { get; set; }
        public string nameTherapist { get; set; }
        public string tiemeFrom { get; set; }
        public string timeTo { get; set; }
        public double costService { get; set; }
    }

    public class BookingDetailDto
    {
        public int idAppointment { get; set; }
        public string nameCustomer { get; set; }
        public string outletName { get; set; }
        public string dateBooking  { get; set; }
        public string timeBooking { get; set; }
        public string statusBooking { get; set; }
        public double cost { get; set; }
        public bool paid { get; set; }
        public List<ServiceAndTherapsitDto> infoService { get; set; }

    }
}
