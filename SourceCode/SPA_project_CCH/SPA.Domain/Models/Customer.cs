namespace SPA.Domain.Models
{
    using SPA.Domain.Audit;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer:IDbModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Appointments = new HashSet<Appointment>();
            ReviewOutlets = new HashSet<ReviewOutlet>();
            ReviewServices = new HashSet<ReviewService>();
            ReviewStaffs = new HashSet<ReviewStaff>();
        }

        public byte[] Avatar { get; set; }

        public int? blackList { get; set; }

        public DateTime? DoB { get; set; }

        [StringLength(25)]
        public string Email { get; set; }

        public int? Gender { get; set; }

        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? NRIC { get; set; }

        public string passWord { get; set; }

        [StringLength(10)]
        public string Phone { get; set; }

        public int? Profression { get; set; }

        public string Token { get; set; }

        public int? Deleted { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public decimal? RecordVersion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> Appointments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReviewOutlet> ReviewOutlets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReviewService> ReviewServices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReviewStaff> ReviewStaffs { get; set; }
    }
}
