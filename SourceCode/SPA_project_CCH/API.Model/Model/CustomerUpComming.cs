using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPA.Domain.Models;

namespace API.Model.Model
{
    public class CustomerUpComming
    {
        

        public string Name { get; set; }

        public int AppointmentID { get; set; }

        public string Phone { get; set; }

        public byte[] Avatar { get; set; }

        public string Services { get; set; }

        public int Room { get; set; }
        public int Bed { get; set; }
    }
}
