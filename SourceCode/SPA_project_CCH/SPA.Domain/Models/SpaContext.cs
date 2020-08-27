namespace SPA.Domain.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SpaContext : DbContext
    {
        public SpaContext()
            : base(@"name=SpaContext")
        {
        }

        public virtual DbSet<Staff_Service> Staff_Services { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<AppointmentDetail> AppointmentDetails { get; set; }
        public virtual DbSet<Bed> Beds { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerType> CustomerTypes { get; set; }
        public virtual DbSet<Measurement> Measurements { get; set; }
        public virtual DbSet<Outlet> Outlets { get; set; }
        public virtual DbSet<Outlet_Staff> Outlet_Staff { get; set; }
        public virtual DbSet<ReviewOutlet> ReviewOutlets { get; set; }
        public virtual DbSet<ReviewService> ReviewServices { get; set; }
        public virtual DbSet<ReviewStaff> ReviewStaffs { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Service_Bed> Service_Bed { get; set; }
        public virtual DbSet<ServiceType> ServiceTypes { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<TimeSlot> TimeSlots { get; set; }
        public virtual DbSet<BufferTime> BufferTimes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .Property(e => e.RecordVersion)
                .HasPrecision(10, 0);

            modelBuilder.Entity<Appointment>()
                .HasMany(e => e.AppointmentDetails)
                .WithRequired(e => e.Appointment1)
                .HasForeignKey(e => e.Appointment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AppointmentDetail>()
                .Property(e => e.RecordVersion)
                .HasPrecision(10, 0);

            modelBuilder.Entity<AppointmentDetail>()
                .HasMany(e => e.Measurements)
                .WithRequired(e => e.AppointmentDetail)
                .HasForeignKey(e => new { e.Appointment, e.Service })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bed>()
                .Property(e => e.RecordVersion)
                .HasPrecision(10, 0);

            modelBuilder.Entity<Bed>()
                .HasMany(e => e.AppointmentDetails)
                .WithOptional(e => e.Bed1)
                .HasForeignKey(e => e.Bed);

            modelBuilder.Entity<Bed>()
                .HasMany(e => e.Service_Bed)
                .WithRequired(e => e.Bed1)
                .HasForeignKey(e => e.Bed)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.passWord)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Phone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Token)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.RecordVersion)
                .HasPrecision(10, 0);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Appointments)
                .WithOptional(e => e.Customer1)
                .HasForeignKey(e => e.Customer);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.ReviewOutlets)
                .WithRequired(e => e.Customer1)
                .HasForeignKey(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.ReviewServices)
                .WithRequired(e => e.Customer1)
                .HasForeignKey(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.ReviewStaffs)
                .WithRequired(e => e.Customer1)
                .HasForeignKey(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CustomerType>()
                .Property(e => e.RecordVersion)
                .HasPrecision(10, 0);

            modelBuilder.Entity<CustomerType>()
                .HasMany(e => e.BufferTimes)
                .WithRequired(e => e.CustomerType1)
                .HasForeignKey(e => e.customerType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Measurement>()
                .Property(e => e.RecordVersion)
                .HasPrecision(10, 0);

            modelBuilder.Entity<Outlet>()
                .Property(e => e.Phone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Outlet>()
                .Property(e => e.RecordVersion)
                .HasPrecision(10, 0);

            modelBuilder.Entity<Outlet>()
                .HasMany(e => e.Outlet_Staff)
                .WithRequired(e => e.Outlet1)
                .HasForeignKey(e => e.Outlet)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Outlet>()
                .HasMany(e => e.ReviewOutlets)
                .WithRequired(e => e.Outlet1)
                .HasForeignKey(e => e.Outlet)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Outlet>()
                .HasMany(e => e.Rooms)
                .WithOptional(e => e.Outlet1)
                .HasForeignKey(e => e.Outlet);

            modelBuilder.Entity<Outlet_Staff>()
                .Property(e => e.RecordVersion)
                .HasPrecision(10, 0);

            modelBuilder.Entity<ReviewOutlet>()
                .Property(e => e.RecordVersion)
                .HasPrecision(10, 0);

            modelBuilder.Entity<ReviewService>()
                .Property(e => e.RecordVersion)
                .HasPrecision(10, 0);

            modelBuilder.Entity<ReviewStaff>()
                .Property(e => e.RecordVersion)
                .HasPrecision(10, 0);

            modelBuilder.Entity<Room>()
                .Property(e => e.RecordVersion)
                .HasPrecision(10, 0);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.Beds)
                .WithOptional(e => e.Room1)
                .HasForeignKey(e => e.Room);

            modelBuilder.Entity<Service>()
                .Property(e => e.RecordVersion)
                .HasPrecision(10, 0);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.AppointmentDetails)
                .WithRequired(e => e.Service1)
                .HasForeignKey(e => e.Service)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.ReviewServices)
                .WithOptional(e => e.Service1)
                .HasForeignKey(e => e.Service);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.BufferTimes)
                .WithRequired(e => e.Service1)
                .HasForeignKey(e => e.Service)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.Service_Bed)
                .WithRequired(e => e.Service1)
                .HasForeignKey(e => e.Service)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.Staff_Services)
                .WithRequired(e => e.Service1)
                .HasForeignKey(e => e.Service)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Service_Bed>()
                .Property(e => e.RecordVersion)
                .HasPrecision(10, 0);

            modelBuilder.Entity<ServiceType>()
                .Property(e => e.RecordVersion)
                .HasPrecision(10, 0);

            modelBuilder.Entity<ServiceType>()
                .HasMany(e => e.Services)
                .WithOptional(e => e.ServiceType1)
                .HasForeignKey(e => e.serviceType);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Experient)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.passWord)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Phone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.userName)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.RecordVersion)
                .HasPrecision(10, 0);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.AppointmentDetails)
                .WithRequired(e => e.Staff1)
                .HasForeignKey(e => e.Staff)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.Outlet_Staff)
                .WithRequired(e => e.Staff1)
                .HasForeignKey(e => e.Staff)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.Staff_Services)
                .WithRequired(e => e.Staff1)
                .HasForeignKey(e => e.Staff)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.ReviewStaffs)
                .WithRequired(e => e.Staff1)
                .HasForeignKey(e => e.Staff)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TimeSlot>()
                .Property(e => e.RecordVersion)
                .HasPrecision(10, 0);

            modelBuilder.Entity<TimeSlot>()
                .HasMany(e => e.AppointmentDetails)
                .WithRequired(e => e.TimeSlot1)
                .HasForeignKey(e => e.timeSlot)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BufferTime>()
                .Property(e => e.RecordVersion)
                .HasPrecision(10, 0);

            modelBuilder.Entity<Staff_Service>()
                .Property(e => e.RecordVersion)
                .HasPrecision(10, 0);

            modelBuilder.Properties<DateTime>().
                Configure(c => c.HasColumnType("datetime2"));
        }
    }
}
