using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.Model
{
    public class TherapistProfile
    {
        public int ID { get; set; }
        public byte[] Avatar { get; set; }
        public string Decription { get; set; }
        public DateTime DayOfBirth { get; set; }
        public string Experience { get; set; }
        public int Gender { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
