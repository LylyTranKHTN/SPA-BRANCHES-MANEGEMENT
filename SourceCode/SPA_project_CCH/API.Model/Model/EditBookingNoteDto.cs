using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.Model
{
    public class EditBookingNoteDto
    {
        public IEnumerable<EditBookingDto> editBookingDtos { get; set; }
        public string note { get; set; }
    }
}
