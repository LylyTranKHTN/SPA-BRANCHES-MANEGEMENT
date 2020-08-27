using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model.HangdingCodeModel
{
    public class PagingInfo
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalPageCount { get; set; }

        public long TotalRecordCount { get; set; }
    }
}
