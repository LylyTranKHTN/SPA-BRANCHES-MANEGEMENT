using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.Model
{
     public class TherapistBookingDto
    {
        public int therapistID { get; set; }
        public string therapistName { get; set; }
        public byte[] therapistAvatar { get; set; }
    }
}
