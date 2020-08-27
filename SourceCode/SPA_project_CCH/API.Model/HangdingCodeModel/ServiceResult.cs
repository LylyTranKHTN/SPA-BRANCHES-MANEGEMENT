using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.HangdingCodeModel
{
    public class ServiceResult<TResult> where TResult: class
    {
        public bool Success { get; set; }

        TResult Result { get; set; }
    }
}
