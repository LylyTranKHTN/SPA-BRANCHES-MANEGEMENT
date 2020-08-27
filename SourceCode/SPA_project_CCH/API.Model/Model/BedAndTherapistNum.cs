using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.Model
{
    public class BedAndTherapistNum
    {
        public int numBed { get; set; }
        public int numTherapist { get; set; }

        public BedAndTherapistNum(int numBed, int numTherapist)
        {
            this.numBed = numBed;
            this.numTherapist = numTherapist;
        }
    }
}
