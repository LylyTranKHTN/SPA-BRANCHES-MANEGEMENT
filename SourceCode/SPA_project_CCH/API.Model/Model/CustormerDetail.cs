using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.Model
{
    public class CustomerDetail
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Black List is required")]
        public int blackList { get; set; }

        [Display(Name = "Day of birth")]
        public DateTime DoB { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public int Gender { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public byte[] Avatar { get; set; }
        public int NRIC { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(11)]
        public string Phone { get; set; }
        public int Profression { get; set; }
    }
}
