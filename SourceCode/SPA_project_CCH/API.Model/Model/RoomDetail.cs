using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Model.Model
{
    public class RoomDetail
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int numOfBed { get; set; }
        public int Outlet { get; set; }

    }
}