using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.Model
{
    public class BookingDTO
    {
        public int serviceID { get; set; }
        public string serviceName { get; set; }
        public byte[] serviceImage { get; set; }
        public TherapistBookingDto therapist { get; set; }
        public TimeSlotDTO timeSlot { get; set; }
    }
}
