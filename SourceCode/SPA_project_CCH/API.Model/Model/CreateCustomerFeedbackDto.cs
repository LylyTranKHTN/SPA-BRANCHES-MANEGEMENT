using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.Model
{
    public class CreateCustomerFeedbackDto
    {
        [Required]
        public int serviceId;

        [Required]
        public int appointmentId;

        [Required]
      
        public string feedback;
    }
}
