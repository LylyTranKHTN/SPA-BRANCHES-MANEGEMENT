using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.Model
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        //position
        public string Type { get; set; }
    }
}
