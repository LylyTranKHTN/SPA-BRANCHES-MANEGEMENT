using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.Model
{
    public class CreateReviewOutletDto
    {
        [Required]
        public int outletId;

        [Required]
        public int customerid;

        [Required]
        public int star;
        public string content;
    }
}
