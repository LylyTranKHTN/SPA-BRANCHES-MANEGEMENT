using SPA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.Repository.Repository
{
    public interface IAppoitmentDetailRepository:IGenericRepository<AppointmentDetail>
    {
        Task<List<AppointmentDetail>> GetAppointmentIncludeServiceAndOutlet(int idoutlet, int idtherapist,  int dayPassed, string nameCustomer);
        Task<List<AppointmentDetail>> GetAppointmentIncludeServiceAndOutletForCustomer(int idcustomer, int dayPassed);
        Task<List<AppointmentDetail>> GetAppointmentIncludeTherapsitAndOutlet(int IDCustomer, int IDService);
        Task<List<AppointmentDetail>> GetAppointmentDetailIncludeCustomerAppointmentService(int outlet, int therapist, DateTime date);
        Task<List<AppointmentDetail>> GetAppointmentIncludeCustomerAppointmentService(int outlet);
        Task<List<AppointmentDetail>> GetBookingDetailInluceServiceTherapist(int idAppoinment);
        Task<List<AppointmentDetail>> GetCustomerByOutletAndText(int idoutlet, string txt);
        Task<List<AppointmentDetail>> GetAllCustomerByOutlet(int idoutlet);
    }

   
    public class AppoitmentDetailRepository : GenericRepository<AppointmentDetail>, IAppoitmentDetailRepository
    {
        public async Task<List<AppointmentDetail>> GetAppointmentIncludeServiceAndOutlet(int idoutlet, int idtherapist, int dayPassed,string nameCustomer)
        {
            var query = DbSet.Where(x => x.Bed1.Room1.Outlet == idoutlet
                                         && (EntityFunctions.DiffDays(DateTime.Today, x.Date)) <= dayPassed
                                         && x.Staff == idtherapist
                                         && x.Appointment1.Customer1.Name.Contains(nameCustomer))
                            .Include(x => x.Service1).Include(x => x.Bed1).Include(x => x.Bed1.Room1).Include(x => x.Bed1.Room1.Outlet1)
                            .Include(x => x.Appointment1)
                            .Include(x => x.Appointment1.Customer1);
            return await query.ToListAsync();
        }
        public async Task<List<AppointmentDetail>> GetAppointmentIncludeServiceAndOutletForCustomer(int idcustomer, int dayPassed)
        {
            var query = DbSet.Where(x => (EntityFunctions.DiffDays(DateTime.Today, x.Date)) <= dayPassed
                                         && x.Appointment1.Customer == idcustomer)
                            .Include(x => x.Service1).Include(x => x.Bed1).Include(x => x.Bed1.Room1).Include(x => x.Bed1.Room1.Outlet1)
                            .Include(x => x.Appointment1);
            return await query.ToListAsync();
        }
        public async Task<List<AppointmentDetail>> GetAppointmentIncludeTherapsitAndOutlet(int IDCustomer, int IDService)
        {
            var query = DbSet.Where(x => x.Appointment1.Customer == IDCustomer && x.Service == IDService)
                                    .Include(x => x.Service1).Include(x => x.Bed1).Include(x => x.Bed1.Room1).Include(x => x.Bed1.Room1.Outlet1)
                                    .Include(x => x.Staff1);
            return await query.ToListAsync();
        }

        public async Task<List<AppointmentDetail>> GetAppointmentDetailIncludeCustomerAppointmentService(int outlet, int therapist, DateTime date)
        {
            var query = DbSet.Where(x => x.Bed1.Room1.Outlet == outlet && x.Staff == therapist && x.Date == date)
                             .Include(x => x.Appointment1).Include(x => x.Service1).Include(x => x.Bed1)
                             .Include(x => x.Appointment1.Customer1)
                             .Include(x => x.Bed1.Room1.Outlet1);

            return await query.ToListAsync();
        }
        public async Task<List<AppointmentDetail>> GetCustomerByOutletAndText(int idoutlet, string txt)
        {
                var query = DbSet.Where(x => (x.Appointment1.Customer1.Name.Contains(txt) || x.Appointment1.Customer1.Phone.Contains(txt) ||
                               x.Appointment1.Customer1.Email.Contains(txt) || ((x.Appointment1.Customer1.NRIC).ToString()).Contains(txt)
                               ) && x.Bed1.Room1.Outlet == idoutlet);
                return await query.ToListAsync();

        }

        public async Task<List<AppointmentDetail>> GetAllCustomerByOutlet(int idoutlet)
        {
                var query = DbSet.Where(x => x.Bed1.Room1.Outlet == idoutlet);
                return await query.ToListAsync();
    

        }

        public async Task<List<AppointmentDetail>> GetBookingDetailInluceServiceTherapist(int idAppointment)
        {
            var query = DbSet.Where(x => x.Appointment == idAppointment)
                .Include(x => x.Staff1)
                .Include(x => x.Service1)
                .Include(x => x.TimeSlot1)
                .Include(x => x.Bed1.Room1.Outlet1);
            return await query.ToListAsync();
        }

        public async Task<List<AppointmentDetail>> GetAppointmentIncludeCustomerAppointmentService(int outlet)
        {
            var query = DbSet.Where(x => x.Bed1.Room1.Outlet == outlet)
                             .Include(x => x.Appointment1).Include(x => x.Service1).Include(x => x.Bed1)
                             .Include(x => x.Appointment1.Customer1);

            return await query.ToListAsync();
        }
    }
}
