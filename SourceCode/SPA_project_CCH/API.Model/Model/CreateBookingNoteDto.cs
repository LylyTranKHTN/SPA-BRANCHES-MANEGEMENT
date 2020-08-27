using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.Model
{
    public class CreateBookingNoteDto
    {
        public IEnumerable<CreateBookingDto> createBookingDtos { get; set; }
        public string note { get; set; }
    }
}
