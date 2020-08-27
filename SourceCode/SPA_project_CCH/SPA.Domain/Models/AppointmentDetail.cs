namespace SPA.Domain.Models
{
    using SPA.Domain.Audit;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppointmentDetail")]
    public partial class AppointmentDetail:IDbModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AppointmentDetail()
        {
            Measurements = new HashSet<Measurement>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Appointment { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Service { get; set; }

        public double? Cost { get; set; }

        public int BufferTime { get; set; }

        [StringLength(200)]
        public string CustomerSign { get; set; }

        public DateTime Date { get; set; }

        [StringLength(500)]
        public string feedBack { get; set; }

        public byte[] imageAfter1 { get; set; }

        public byte[] imageAfter2 { get; set; }

        public byte[] imageBefore { get; set; }

        [StringLength(200)]
        public string Note { get; set; }

        public int? Bed { get; set; }

        public int timeSlot { get; set; }

        public int Staff { get; set; }

        public int? Deleted { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public decimal? RecordVersion { get; set; }

        public virtual Appointment Appointment1 { get; set; }

        public virtual Bed Bed1 { get; set; }

        public virtual Service Service1 { get; set; }

        public virtual Staff Staff1 { get; set; }

        public virtual TimeSlot TimeSlot1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Measurement> Measurements { get; set; }
    }
}
