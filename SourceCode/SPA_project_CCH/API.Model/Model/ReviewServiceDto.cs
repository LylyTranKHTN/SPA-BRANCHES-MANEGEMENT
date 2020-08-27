using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.Model
{
    public class ReviewServiceDto
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public int? Star { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}
