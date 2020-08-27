using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace API.Model.Enum
{
    public enum BookingHistoryStatus
    {
        Booked = 1,
        Canceled = 0,
        Performing = 2,
        Completed = 3,
        NoShow = 4,
        
    }
}
