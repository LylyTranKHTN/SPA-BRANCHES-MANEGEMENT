using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.Model
{
    public class CustomerRegiter
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        public int Gender { get; set; }

       
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public int NRIC { get; set; }

        [Required]
        public string PassWord { get; set; }

        [Required]
        public string ConfirmPassWord { get; set; }
    }
}
