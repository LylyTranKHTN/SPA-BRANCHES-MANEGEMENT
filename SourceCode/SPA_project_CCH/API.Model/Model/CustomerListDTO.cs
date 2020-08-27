using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.Model
{
    public class CustomerListDTO
    {
        public byte[] avt { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phonenumber { get; set; }

        public string nric { get; set; }

    }
}
