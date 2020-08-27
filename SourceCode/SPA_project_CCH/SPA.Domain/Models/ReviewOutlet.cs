namespace SPA.Domain.Models
{
    using SPA.Domain.Audit;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReviewOutlet")]
    public partial class ReviewOutlet: IDbModel
    {
        public ReviewOutlet()
        {

        }

        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        public DateTime? Date { get; set; }

        public int ID { get; set; }

        public int? Star { get; set; }

        public int Outlet { get; set; }

        public int Customer { get; set; }

        public int? Deleted { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public decimal? RecordVersion { get; set; }

        public virtual Customer Customer1 { get; set; }

        public virtual Outlet Outlet1 { get; set; }
    }
}
