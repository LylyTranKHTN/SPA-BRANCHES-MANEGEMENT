using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.Model
{
    public class BookingPhotoDTO
    {

        public int AppointmentId { get; set; }

        public byte[] imageAfter1 { get; set; }

        public byte[] imageAfter2 { get; set; }

        public byte[] imageBefore { get; set; }
    }
}
