using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.Domain.Audit
{
    public interface IDbModel
    {
        int? Deleted { get; set; }
        DateTime? CreateDate { get; set; } // CreatedDate
        DateTime? UpdateDate { get; set; } // UpdatedDate
    }
}
