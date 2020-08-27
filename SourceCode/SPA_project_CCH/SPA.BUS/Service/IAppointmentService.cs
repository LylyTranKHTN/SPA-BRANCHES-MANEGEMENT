using API.Model.Model;
using SPA.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.BUS.Service
{
    public interface IAppointmentService
    {
        IRepositoryHelper _repositoryHelper { get; }

        /// <summary>
        /// Find list bookings can book
        /// </summary>
        /// <param name="outletId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<LogicResult<IEnumerable<BookingDTO>>> GetBookingByOutletAndDate(int outletId, DateTime date);

        /// <summary>
        /// Find list of timeslot is available
        /// </summary>
        /// <param name="outletId"></param>
        /// <param name="date"></param>
        /// <param name="service"></param>
        /// <param name="therapist"></param>
        /// <returns></returns>
        Task<LogicResult<IEnumerable<TimeSlotDTO>>> GetTimeslotByOutlet_Date_Service_Therapist(int outletId, DateTime date, int service, int therapist);

        /// <summary>
        /// Find list of therapist is available
        /// </summary>
        /// <param name="outletId"></param>
        /// <param name="date"></param>
        /// <param name="service"></param>
        /// <param name="timeSlot"></param>
        /// <returns></returns>
        Task<LogicResult<IEnumerable<TherapistBookingDto>>> GetTherapistByOutlet_Date_Service_TimeSlot(int outletId, DateTime date, int service, int timeSlot);

        /// <summary>
        /// Get list of booking history
        /// </summary>
        /// <param name="IDOutlet"></param>
        /// <param name="dayPassed"></param>
        /// <param name="IDTherapist"></param>
        /// <returns></returns>
        Task<LogicResult<IEnumerable<Booking_history_of_customersGroupDto>>> GetHistoryBooking(int IDTherapist,int IDOutlet, int dayPassed, string nameCustomer);

        /// <summary>
        /// Post booking
        /// </summary>
        /// <param name="bookings"></param>
        /// <returns></returns>
        Task<LogicResult<IEnumerable<CreateBookingDto>>> CreateBooking(IEnumerable<CreateBookingDto> bookings, int customerID, int outletID, string note);

        Task<LogicResult> CancelBooking(int appointmentID);

        Task<LogicResult<IEnumerable<Booking_history_of_customersGroupDto>>> GetHistoryBookingCustomer(int IDCustomer, int dayPassed);

        /// <summary>
        /// Get signature of customer by appointment
        /// </summary>
        /// <param name="appointmentID"></param>
        /// <returns></returns>
        Task<LogicResult<BookingCustomerSignature>> GetCustomerSignature(int appointmentID);


        Task<LogicResult<IEnumerable<MeasurementCustomerDto>>> GetMeasurementByCustomer(int IDCustomer, int IDservice);

        /// <summary>
        /// Edit customer
        /// </summary>
        /// <param name="bookings"></param>
        /// <param name="customerID"></param>
        /// <param name="outletID"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        Task<LogicResult<IEnumerable<Booking_history_of_customersGroupDto>>> EditBooking(IEnumerable<EditBookingDto> bookings, int customerID, int outletID, string note);

        /// <summary>
        /// PutMeasurement
        /// </summary>
        /// <param name="measurementCDTO"></param>
        /// <returns></returns>
        Task<LogicResult> PutMeasurement(MeasurementCustomerDto measurementCDTO);
        Task<LogicResult<BookingDetailDto>> GetBookingAndServiceInfo(int appointmentID);

        /// <summary>
        /// GetCustomerFeedback
        /// </summary>
        /// <param name="appointmentID"></param>
        /// <returns></returns>
        //Task<LogicResult<IEnumerable<AppointmentDetailDto>>> GetCustomerFeedback(int appointmentID,int serviceId);
        Task<LogicResult<IEnumerable<AppointmentDetailDto>>> GetCustomerFeedback(int customerId, int serviceId);

        /// <summary>
        /// CreateCustomerFeedback
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        Task<LogicResult> CreateCustomerFeedback(int appointmentId,int outletId,string feedback);

        /// <summary>
        /// Change status of appointment
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<LogicResult> ChangeStatus(int appointmentId, int status);


        /// <summary>
        /// getPhoto of appointment
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns></returns>

        Task<LogicResult<BookingPhotoDTO>> GetCustomerPhoto(int appointmentID);


        /// <summary>
        /// addnote of appointment
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <param name="Note"></param>
        /// <returns></returns>
        Task<LogicResult> AddNote(AppointmentNote appointmentNote);

        /// <summary>
        /// Signature of appointment
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <param name="Signature"></param>
        /// <returns></returns>
        Task<LogicResult> UpdateSignature(CustomerSignDTO customerSignDTO);
    }
}
