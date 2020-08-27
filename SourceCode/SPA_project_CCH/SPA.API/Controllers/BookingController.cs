using API.Model;
using API.Model.HangdingCodeModel;
using API.Model.Model;
using AutoMapper;
using SPA.API.Authentication;
using SPA.BUS.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SPA.API.Controllers
{
    [RoutePrefix("booking")]
    //[Authorize]
    public class BookingController : BaseController
    {

        private readonly IMapper _mapper;
        private readonly IAppointmentService _appointmentService;
        private readonly ICustomerService _customerService;
        private readonly IOutletService _outletService;

        public BookingController(IMapper mapper, IAppointmentService appointmentService, IOutletService outletService, ICustomerService customerService)
        {
            _mapper = mapper;
            _appointmentService = appointmentService;
            _customerService = customerService;
            _outletService = outletService;
        }
        [HttpGet]
        [Authorize(Roles ="customer")]
        [Route("history/{idtherapist}/{idoutlet}/{dayPassed}/{nameCustomer}")]
        public async Task<HttpResponseMessage> GetBookingHistoryTherapist(int idtherapist, int idoutlet, int dayPassed, string nameCustomer)
        {
            var messege = CreateMessageData($"booking/history/{idtherapist}/{idoutlet}/{dayPassed}/{nameCustomer}", new KeyValuePair<string, string>[]{
                new KeyValuePair<string, string>("idtherapist", idtherapist.ToString()),
                                                new KeyValuePair<string, string>("idoutlet", idoutlet.ToString()),
                                                new KeyValuePair<string, string>("dayPassed", dayPassed.ToString()),
                                                new KeyValuePair<string, string>("nameCustomer", nameCustomer)
            });
            var historybooking = await _appointmentService.GetHistoryBooking(idtherapist,idoutlet, dayPassed, nameCustomer);
            if (!historybooking.IsSuccess)
            {
                return CreateValidationErrorResponse(messege, new ValidationResult(historybooking.message));
            }
            if (historybooking.Result == null)
            {
                return CreateNotFoundResponse(messege, Validation.FileNotFound);
            }
            return CreateOkResponse(messege, historybooking.Result);
        }

        [HttpGet]
        [Route("history/{idcustomer}/{dayPassed}")]
        public async Task<HttpResponseMessage> GetBookingHistoryCustomer(int idcustomer, int dayPassed)
        {
            var messege = CreateMessageData($"booking/history/{idcustomer}/{dayPassed}", new KeyValuePair<string, string>[]{
                new KeyValuePair<string, string>("idcustomer", idcustomer.ToString()),
                                                new KeyValuePair<string, string>("dayPassed", dayPassed.ToString()),
            });
            var historybooking = await _appointmentService.GetHistoryBookingCustomer(idcustomer, dayPassed);
            if (!historybooking.IsSuccess)
            {
                return CreateValidationErrorResponse(messege, new ValidationResult(historybooking.message));
            }
            if (historybooking.Result == null)
            {
                return CreateNotFoundResponse(messege, Validation.FileNotFound);
            }
            return CreateOkResponse(messege, historybooking.Result);
        }

        [HttpGet]
        [Route("booking-available/therapist/{outletId}/{serviceId}/{timeslot}/{date:datetime}")]
        public async Task<HttpResponseMessage> getTherapistByServiceAndTimeslot(int outletId, int serviceId, int timeslot, DateTime date)
        {
            var messege = CreateMessageData($"booking/getTherapist/{outletId}/{serviceId}/{timeslot}/{date}",
                                            new KeyValuePair<string, string>[] {
                                                new KeyValuePair<string, string>("outletId", outletId.ToString()),
                                                new KeyValuePair<string, string>("serviceId", serviceId.ToString()),
                                                new KeyValuePair<string, string>("timeslot", timeslot.ToString()),
                                                new KeyValuePair<string, string>("date", date.ToString())
                                            });

            var therapists = await _appointmentService.GetTherapistByOutlet_Date_Service_TimeSlot(outletId, date, serviceId, timeslot);
            if (!therapists.IsSuccess)
                return CreateValidationErrorResponse(messege, new ValidationResult(therapists.message));

            return CreateOkResponse(messege, therapists);
        }

        [HttpGet]
        [Route("booking-available/timeslot/{outletId}/{serviceId}/{therapistId}/{date:datetime}")]
        public async Task<HttpResponseMessage> getTimeSlotByServiceAndTherapist(int outletId, int serviceId, int therapistId, DateTime date)
        {
            var messege = CreateMessageData($"booking/getTherapist/{outletId}/{serviceId}/{therapistId}/{date}",
                                            new KeyValuePair<string, string>[] {
                                                new KeyValuePair<string, string>("outletId", outletId.ToString()),
                                                new KeyValuePair<string, string>("serviceId", serviceId.ToString()),
                                                new KeyValuePair<string, string>("therapistId", therapistId.ToString()),
                                                new KeyValuePair<string, string>("date", date.ToString()),
                                            });

            var timeslots = await _appointmentService.GetTimeslotByOutlet_Date_Service_Therapist(outletId, date, serviceId, therapistId);
            if (!timeslots.IsSuccess)
            {
                return CreateValidationErrorResponse(messege, new ValidationResult(timeslots.message));
            }

            return CreateOkResponse(messege, timeslots.Result);
        }

        [HttpGet]
        [Route("booking-available/{outletId}/{date:datetime}/{pagesize=int?}/{pageNumber=int?}")]
        public async Task<HttpResponseMessage> getBookingAvailable(int outletId, DateTime date, int? pageSize, int? pageNumber)
        {
            var messege = CreateMessageData($"booking/{outletId}/{date}/{pageSize}/{pageNumber}",
                                            new KeyValuePair<string, string>[] {
                                                new KeyValuePair<string, string>("outletId", outletId.ToString()),
                                                new KeyValuePair<string, string>("date", date.ToString()),
                                                new KeyValuePair<string, string>("pageSize", pageSize.ToString()),
                                                new KeyValuePair<string, string>("pageNumber", pageNumber.ToString())
                                            });

            var bookings = await _appointmentService.GetBookingByOutletAndDate(outletId, date);

            if (!bookings.IsSuccess)
                return CreateValidationErrorResponse(messege, new ValidationResult(bookings.message));

            var numOfRecords = bookings.Result.Count();

            if (pageSize.HasValue && pageNumber.HasValue)
            {
                if (numOfRecords <= ((pageSize - 1) * (pageNumber - 1)))
                {
                    return CreateBadRequestErrorResponse(messege, Validation.InvalidPageIndex);
                }

                var result = bookings.Result.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);

                return CreateOkResponse(messege, result, pageNumber, pageSize, bookings.Result.Count());
            }

            return CreateOkResponse(messege, bookings.Result);
        }

        /// API
        /// {
        ///	"createBookingDtos": [
        ///		{
        ///			"serviceID" : 2,
        ///			"therapistID" : 1,
        ///			"timeSlotID" : 1,
        ///			"bookingDate" : "12/18/2018",
        ///     },
        ///		{
        ///			"serviceID" : 2,
        ///			"therapistID" : 2,
        ///			"timeSlotID" : 1,
        ///			"bookingDate" : "12/18/2018",
        ///		}
        ///	],
        ///	"note":"Maybe i will late"
        /// }
        [HttpPost]
        [Route("{customerID}/{outletID}")]
        public async Task<HttpResponseMessage> CreateBooking(CreateBookingNoteDto booking, int customerID, int outletID)
        {
            var messege = CreateMessageData($"booking/{customerID}/{outletID}", 
                new KeyValuePair<string, string>[] {
                                            new KeyValuePair<string, string>("customerID",customerID.ToString()),
                                            new KeyValuePair<string, string>("outletID",outletID.ToString())});
            if (!ModelState.IsValid || customerID <= 0 || outletID <= 0)
            {
                return CreateValidationErrorResponse(messege, new ValidationResult(Validation.InvalidParameters));
            }

            var customer = await _customerService.GetCustomerById(customerID);
            if (!customer.IsSuccess)
            {
                return CreateValidationErrorResponse(messege, new ValidationResult(customer.message));
            }

            var outlet = await _outletService.GetOutletById(outletID);
            if (!outlet.IsSuccess)
            {
                return CreateValidationErrorResponse(messege, new ValidationResult(outlet.message));
            }

            var bookingResult = await _appointmentService.CreateBooking(booking.createBookingDtos, customerID, outletID, booking.note);
            if (!bookingResult.IsSuccess)
            {
                return CreateValidationErrorResponse(messege, new ValidationResult(bookingResult.message));
            }
            if (bookingResult.Result.Count() != booking.createBookingDtos.Count())
                return CreateResponse(HttpStatusCode.OK, new ErrorResponseModel() { Message = Validation.SomeBookingSoldOut,
                                                                                    StatusCode = (int)HttpStatusCode.OK,
                                                                                    StatusDescription = HttpStatusCode.OK.ToString() }, messege, bookingResult.Result);
            return CreateOkResponse(messege, bookingResult.Result);
        }

        [HttpPut]
        [Route("{customerID}/{outletID}")]
        public async Task<HttpResponseMessage> EditBooking (EditBookingNoteDto booking, int customerID, int outletID)
        {
            var messege = CreateMessageData($"booking/{customerID}/{outletID}", new KeyValuePair<string, string>[] {
                                            new KeyValuePair<string, string>("customerID",customerID.ToString()),
                                            new KeyValuePair<string, string>("outletID",outletID.ToString())});
            if (!ModelState.IsValid || customerID <= 0 || outletID <= 0)
            {
                return CreateValidationErrorResponse(messege, new ValidationResult(Validation.InvalidParameters));
            }

            var customer = await _customerService.GetCustomerById(customerID);
            if (!customer.IsSuccess)
            {
                return CreateValidationErrorResponse(messege, new ValidationResult(customer.message));
            }

            var outlet = await _outletService.GetOutletById(outletID);
            if (!outlet.IsSuccess)
            {
                return CreateValidationErrorResponse(messege, new ValidationResult(outlet.message));
            }

            var bookingResult = await _appointmentService.EditBooking(booking.editBookingDtos, customerID, outletID, booking.note);
            if (!bookingResult.IsSuccess)
            {
                return CreateSystemErrorResponse(messege);
            }

            return CreateOkResponse(messege, bookingResult.Result);
        }

        [HttpGet]
        [Route("customersign/{appointmentID}")]
        public async Task<HttpResponseMessage> GetCustomerAppointment (int appointmentID)
        {
            var message = CreateMessageData($"booking/customersign/{appointmentID}", new KeyValuePair<string, string>("appointmentID", appointmentID.ToString()));
            var customer = await _appointmentService.GetCustomerSignature(appointmentID);

            if(!customer.IsSuccess)
            {
                return CreateValidationErrorResponse(message, new ValidationResult(customer.message));
            }
            
            return CreateOkResponse(message, customer.Result);
        }

        [HttpDelete]
        [Route("{appointmentID}")]
        public async Task<HttpResponseMessage> CancelBooking(int appointmentID)
        {
            var message = CreateMessageData($"booking/{appointmentID}", new KeyValuePair<string, string>("appointmentID",appointmentID.ToString()));

            if (appointmentID <= 0)
                return CreateValidationErrorResponse(message, new ValidationResult(Validation.InvalidParameters));

            var result = await _appointmentService.CancelBooking(appointmentID);
            if (!result.IsSuccess)
            {
                return CreateValidationErrorResponse(message, new ValidationResult(result.message));
            }

            return CreateOkResponse(message, appointmentID);
        }

        [HttpGet]
        [Route("getmeasurement/customer/{IDCustomer}/service/{IDService}")]
        public async Task<HttpResponseMessage> GetMeasurementCustomer(int IDCustomer, int IDService)
        {
            var messege = CreateMessageData($"booking/getmeasurement/customer/{IDCustomer}/service/{IDService}"
                , new KeyValuePair<string, string>[] { new KeyValuePair<string,string>("IDCustomer", IDCustomer.ToString()),
                new KeyValuePair<string,string>("IDService", IDService.ToString())});

            var measurement = await _appointmentService.GetMeasurementByCustomer(IDCustomer, IDService);
            if (!measurement.IsSuccess)
            {
                return CreateValidationErrorResponse(messege, new ValidationResult(measurement.message));
            }
            return CreateOkResponse(messege, measurement.Result);
        }

        [HttpPut]
        [Route("editMeasurement")]
        public async Task<HttpResponseMessage>EditMeasurement(MeasurementCustomerDto measurementCDTO)
        {
            var messageData = CreateMessageData($"booking/editMeasurement");
            if(!ModelState.IsValid)
            {
                return CreateValidationErrorResponse(messageData, new ValidationResult(Validation.InvalidParameters));

            }
            var result = await _appointmentService.PutMeasurement(measurementCDTO);
            if (!result.IsSuccess)
                return CreateSystemErrorResponse(messageData);

            return CreateOkResponse(messageData, measurementCDTO);
        }
        [HttpGet]
        [Route("{iDAppointment}")]
        public async Task<HttpResponseMessage> GetBookingDetail(int iDAppointment)
        {
            var message = CreateMessageData($"booking/{iDAppointment}", new KeyValuePair<string, string>("iDAppointment", iDAppointment.ToString()));
            var measurement = await _appointmentService.GetBookingAndServiceInfo(iDAppointment);
            return CreateOkResponse(message, measurement.Result);
        }

        //[HttpGet]
        //[Route("feedback/{appointmentId}/{serviceId}")]
        //public async Task<HttpResponseMessage> GetCustomerFeedback(int appointmentId,int serviceId)
        //{
        //    var message = CreateMessageData($"booking/feedback/{appointmentId}/{serviceId}", new KeyValuePair<string, string>("appointmentId", appointmentId.ToString()));
        //    var feedback = await _appointmentService.GetCustomerFeedback(appointmentId, serviceId);
        //    if (!feedback.IsSuccess)
        //    {
        //        return CreateValidationErrorResponse(message, new ValidationResult(feedback.message));
        //    }
        //    if (feedback.Result == null)
        //    {
        //        return CreateNotFoundResponse(message, Validation.FileNotFound);
        //    }
        //    return CreateOkResponse(message, feedback.Result);
        //}

        [HttpGet]
        [Route("feedback/{customerId}/{serviceId}")]
        public async Task<HttpResponseMessage> GetCustomerFeedback(int customerId, int serviceId)
        {
            var message = CreateMessageData($"booking/feedback/{customerId}/{serviceId}", new KeyValuePair<string, string>("customerId", customerId.ToString()));
            var feedback = await _appointmentService.GetCustomerFeedback(customerId, serviceId);
            if (!feedback.IsSuccess)
            {
                return CreateValidationErrorResponse(message, new ValidationResult(feedback.message));
            }
            if (feedback.Result == null)
            {
                return CreateNotFoundResponse(message, Validation.FileNotFound);
            }
            return CreateOkResponse(message, feedback.Result);
        }

        [HttpPost]
        [Route("feedback")]
        public async Task<HttpResponseMessage> CreateCustomerFeedback(CreateCustomerFeedbackDto createCustomerFeedback)
        {
            var message = CreateMessageData($"booking/feedback");
            if (!ModelState.IsValid)
                return CreateValidationErrorResponse(message, new ValidationResult(Validation.InvalidParameters));

            var result = await _appointmentService.CreateCustomerFeedback(createCustomerFeedback.appointmentId, createCustomerFeedback.serviceId,createCustomerFeedback.feedback);
            if (!result.IsSuccess)
            {
                return CreateValidationErrorResponse(message, new ValidationResult(result.message));
            }
            return CreateOkResponse(message, createCustomerFeedback);
        }

        [HttpPut]
        [Route("changeStatus/{appointmentId}/{status}")]
        [RoleManage(Roles ="therapist")]
        public async Task<HttpResponseMessage> ChangeStatus(int appointmentId, int status)
        {
            var message = CreateMessageData($"booking/changeStatus/{appointmentId}/{status}", new KeyValuePair<string, string>[]{
                                    new KeyValuePair<string, string>("appointmentId", appointmentId.ToString()),
                                    new KeyValuePair<string, string>("status", status.ToString())});

            var result = await _appointmentService.ChangeStatus(appointmentId, status);
            if (!result.IsSuccess)
                return CreateValidationErrorResponse(message, new ValidationResult(result.message));
            return CreateOkResponse(message, status);
        }

        [HttpGet]
        [Route("getPhoto/{idAppointmentDetail}")]
        public async Task<HttpResponseMessage> GetPhoto(int idAppointmentDetail)
        {
            var message = CreateMessageData($"booking/getPhoto/{idAppointmentDetail}", new KeyValuePair<string, string>("idAppointmentDetail", idAppointmentDetail.ToString()));
            var customer = await _appointmentService.GetCustomerPhoto(idAppointmentDetail);

            if (!customer.IsSuccess)
            {
                return CreateValidationErrorResponse(message, new ValidationResult(customer.message));
            }

            return CreateOkResponse(message, customer.Result);
        }

        [HttpPut]
        [Route("AppointmentNote")]
        public async Task<HttpResponseMessage> AddNote(AppointmentNote appointmentNote)
        {
            var messageData = CreateMessageData($"Booking");

            var result = await _appointmentService.AddNote(appointmentNote);
            if (!result.IsSuccess)
            {
                return CreateValidationErrorResponse(messageData, new ValidationResult(Validation.ServerError));
            }
            return CreateOkResponse(messageData, result.message);

        }

        [HttpPut]
        [Route("customersign")]
        public async Task<HttpResponseMessage> updateSignature(CustomerSignDTO customerSignDTO)
        {
            var messageData = CreateMessageData($"Booking");

            var result = await _appointmentService.UpdateSignature(customerSignDTO);
            if (!result.IsSuccess)
            {
                return CreateValidationErrorResponse(messageData, new ValidationResult(Validation.ServerError));
            }
            return CreateOkResponse(messageData, result.message);

        }
    }
}
