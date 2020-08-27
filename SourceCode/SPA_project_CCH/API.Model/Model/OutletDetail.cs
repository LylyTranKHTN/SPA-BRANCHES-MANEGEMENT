using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Model.Model
{
    public class OutletDetail
    {
        public int ID { get; set; }
        public string address { get; set; }
        public byte[] Image { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int Start { get; set; }
    }
}