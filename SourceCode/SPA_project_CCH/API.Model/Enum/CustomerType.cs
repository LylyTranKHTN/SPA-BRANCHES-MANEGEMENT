using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.Enum
{
    public enum CustomerType
    {
        /// <summary>
        /// dont have any appointment
        /// </summary>
        Fresh = 1,
        /// <summary>
        /// have appointment but not used this service
        /// </summary>
        FirstTime = 2,
        /// <summary>
        /// is used this service
        /// </summary>
        Used = 3
    }
}
