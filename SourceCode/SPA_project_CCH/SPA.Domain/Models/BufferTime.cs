namespace SPA.Domain.Models
{
    using SPA.Domain.Audit;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BufferTime")]
    public partial class BufferTime: IDbModel
    {
        [Column("bufferTime")]
        public int bufferTime1 { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int customerType { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Service { get; set; }

        public int? Deleted { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public decimal? RecordVersion { get; set; }

        public virtual CustomerType CustomerType1 { get; set; }

        public virtual Service Service1 { get; set; }
    }
}
