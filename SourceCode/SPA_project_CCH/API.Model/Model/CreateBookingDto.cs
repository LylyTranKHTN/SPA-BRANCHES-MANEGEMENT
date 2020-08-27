using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.Model
{
    public class CreateBookingDto
    {
        [Required(ErrorMessage = "Service Id is required!")]
        public int serviceID { get; set; }

        [Required(ErrorMessage = "Therapist Id is required!")]
        public int therapistID { get; set; }

        [Required(ErrorMessage = "Timeslot is required!")]
        public int timeSlotID { get; set; }

        [Required(ErrorMessage = "Booking date is required!")]
        public DateTime bookingDate { get; set; }
    }
}
