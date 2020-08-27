using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.Model
{
    public class MsgTwilio
    {
        public string phone { get; set; }
        public string sendfrom { get; set; }
        public string  message { get; set; }
    }
}
