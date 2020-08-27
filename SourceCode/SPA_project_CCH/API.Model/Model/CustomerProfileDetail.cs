using SPA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Model.Model
{
    public class CustomerProfileDetail
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? Gender { get; set; }
        public DateTime? DoB { get; set; }
        public int? Profession { get; set; }
        public string PassWord { get; set; }
        public byte[] Avatar { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? NRIC { get; set; }
        public int? blackList { get; set; }
    }
}
