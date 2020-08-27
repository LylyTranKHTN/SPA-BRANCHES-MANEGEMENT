using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.Model.Model
{
    public class TherapistDetail
    {
        public byte[] Avatar { get; set; }
        public string Decription { get; set; }
        public DateTime DoB { get; set; }
        public string Experience { get; set; }
        public int Possition { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }
        public string Name { get; set; }
        [Required]
        public string passWord { get; set; }
        public string Phone { get; set; }
        [Required]
        public string userName { get; set; }
    }
}