using SPA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Model.Model
{
    public class BedServiceDto
    {
        public int ID { get; set; }
        public int Room { get; set; }
        public List<ServicesIdNameDto> services { get; set; }
    }
}