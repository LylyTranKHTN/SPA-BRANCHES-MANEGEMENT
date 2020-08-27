using API.Model;
using API.Model.Enum;
using API.Model.Model;
using AutoMapper;
using SPA.Domain.Models;
using SPA.Repository.Repository;
using SPA.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerType = API.Model.Enum.CustomerType;

namespace SPA.BUS.Service
{

    public class AppointmentService : IAppointmentService
    {
        public const int NUM_OF_DAY_CANCEL = 2;
        public const int DEFAULT_DAY_PASSED = 30;

        public IRepositoryHelper _repositoryHelper { get; }
        public IMapper _mapper { get; }

        public AppointmentService(IRepositoryHelper repositoryHelper, IMapper mapper)
        {
            _repositoryHelper = repositoryHelper;
            _mapper = mapper;
        }

        public async Task<LogicResult<IEnumerable<BookingDTO>>> GetBookingByOutletAndDate(int outletId, DateTime date)
        {
            if (date.Date < DateTime.Now.Date)
                return new LogicResult<IEnumerable<BookingDTO>>() { IsSuccess = false, message = Validation.DateTimeIsInPast };

            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var appoitmentDetailRepo = _repositoryHelper.GetRepository<IAppoitmentDetailRepository>(unitofwork);
            var serviceRepo = _repositoryHelper.GetRepository<IServiceRepository>(unitofwork);
            var therapistRepo = _repositoryHelper.GetRepository<ITherapistRepository>(unitofwork);

            var result = new List<BookingDTO>();
            //list appointment is booked in this date and this outlet
            var appoitmentBooked = await appoitmentDetailRepo.GetAsync(x => x.Bed1.Room1.Outlet == outletId && x.Date == date);

            //list all service of outlet
            var services = await serviceRepo.GetAsync(x => x.Service_Bed.Any(y => y.Bed1.Room1.Outlet == outletId));

            foreach(var service in services)
            {
                //list all therapist can do this service
                var therapists = await therapistRepo.GetAsync(x => x.Staff_Services.Any(y => y.Service == service.ID) && x.Possition == (int)Position.therapist);
                if (therapists.Any())
                {
                    var timeslotsOfTherapistFree = await GetTimeslotByOutlet_Date_Service_Therapist(outletId, date, service.ID, therapists.First().ID);

                    //if first therapist has all booked in that day, but it's recently occus
                    int temp = 0; //index therapist free
                    if (!timeslotsOfTherapistFree.Result.Any())
                    {
                        for (int i = 0; i <= therapists.Count(); i++)
                        {
                            var timeslots = await GetTimeslotByOutlet_Date_Service_Therapist(outletId, date, service.ID, therapists.ElementAt(i).ID);
                            //exit the loop to get this value if timeslotOfTherapistFree is not null
                            if (timeslots.Result.Any())
                            {
                                i = therapists.Count() + 1;
                                timeslotsOfTherapistFree.Result = timeslots.Result;
                                temp = i;
                            }
                        }
                    }

                    if (timeslotsOfTherapistFree.Result.Any())
                    {
                        var therapist = therapists.ElementAt(temp);
                        var timeslot = timeslotsOfTherapistFree.Result.First();
                        if (date.Date == DateTime.Now.Date)
                        {
                            int timeslotMin = Convert.ToInt32(((DateTime.Now.TimeOfDay - new TimeSpan(7, 15, 0)).TotalMinutes) / 15);
                            timeslot = timeslotsOfTherapistFree.Result.Where(x => x.ID > timeslotMin).First();
                        }

                        //mapping result
                        var bookingDto = new BookingDTO()
                        {
                            serviceID = service.ID,
                            serviceName = service.Name,
                            therapist = new TherapistBookingDto()
                            {
                                therapistID = therapist.ID,
                                therapistAvatar = therapist.Avatar,
                                therapistName = therapist.Name,
                            },
                            serviceImage = service.Image,
                            timeSlot = new TimeSlotDTO()
                            {
                                ID = timeslot.ID,
                                From = timeslot.From,
                                To = timeslot.To
                            }
                        };

                        result.Add(bookingDto);
                    }
                }
            }

            if (!result.Any())
                return new LogicResult<IEnumerable<BookingDTO>>() { IsSuccess = false, message = Validation.AllServiceSoldOut, Result = null };

            return new LogicResult<IEnumerable<BookingDTO>>() { IsSuccess = true, Result = result};
        }

        public async Task<LogicResult<BookingCustomerSignature>> GetCustomerSignature(int appointmentID)
        {
            if (appointmentID <= 0)
                return new LogicResult<BookingCustomerSignature>() { IsSuccess = false, message = Validation.InvalidParameters };
            else
            {
                var unitowork = _repositoryHelper.GetUnitOfWork();
                var appointmentDetailRepo = _repositoryHelper.GetRepository<IAppoitmentDetailRepository>(unitowork);
                var appointmentRepo = _repositoryHelper.GetRepository<IAppoitmentRepository>(unitowork);
                var serviceRepo = _repositoryHelper.GetRepository<IServiceRepository>(unitowork);
                var customerRepo = _repositoryHelper.GetRepository<ICustomerRepository>(unitowork);
                var appointmentDetail = await appointmentDetailRepo.GetByIdAsync(appointmentID);
                var appointment = await appointmentRepo.GetByIdAsync(appointmentID);
                var result = new BookingCustomerSignature()
                {
                    AppointmentId = appointment.ID,
                    CustomerSign = appointmentDetail.CustomerSign

                };
                return new LogicResult<BookingCustomerSignature>() { IsSuccess = true, Result = result };
            }
        }

        public async Task<LogicResult<IEnumerable<TimeSlotDTO>>> GetTimeslotByOutlet_Date_Service_Therapist(int outletId, DateTime date ,int service, int therapist)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var appointmentDetailRepo = _repositoryHelper.GetRepository<IAppoitmentDetailRepository>(unitofwork);
            var serviceRepo = _repositoryHelper.GetRepository<IServiceRepository>(unitofwork);
            var timeslotRepo = _repositoryHelper.GetRepository<ITimeSlotRepository>(unitofwork);

            var currentService = await serviceRepo.GetByIdAsync(service);
            //timeslots start time of service
            var timeslots = await timeslotRepo.GetAsync(x => ((x.ID - 1) % currentService.numOfTimeSlot) == 0);

            //list all appoitments that therapist busy
            var appointments = await appointmentDetailRepo.GetAsync(x => x.Date == date && x.Staff == therapist &&
                                        x.Service == service && x.Bed1.Room1.Outlet == outletId);

            //list all timeslot therapist is busy
            var timeslotBusy = new List<int>();
            if (appointments.Any())
            {
                foreach (var i in appointments)
                {
                    //timeslot
                    for (int j = 0; j < currentService.numOfTimeSlot; j++)
                    {
                        timeslotBusy.Add(i.timeSlot + j);
                    }
                    //buffertime
                    for (int j = 0; j < i.BufferTime; j++)
                    {
                        timeslotBusy.Add(i.timeSlot + currentService.numOfTimeSlot + j);
                    }
                }
            }

            //list all free timeslot
            var freeTimeslots = new List<TimeSlotDTO>();
            foreach (var item in timeslots)
            {
                //temp = 0: valid, temp > 0: invalid
                int temp = 0;

                //check timeslot of one service is not busy
                for (int i = item.ID; i < item.ID + currentService.numOfTimeSlot; i++)
                {
                    if (timeslotBusy.Any(x => x.Equals(i)))
                        temp++;
                }

                //check bed is visible
                if (!IsHasBed(outletId, date, service, item.ID, therapist))
                    temp = 1;

                //add result
                if (temp == 0)
                {
                    var timeslotDto = new TimeSlotDTO()
                    {
                        ID = item.ID,
                        From = item.From.ToString(),
                        To = (item.To + new TimeSpan(0, 15 * currentService.numOfTimeSlot, 0)).ToString(),
                    };

                    freeTimeslots.Add(timeslotDto);
                }
            }

            return new LogicResult<IEnumerable<TimeSlotDTO>>() { IsSuccess = true, Result = freeTimeslots};
        }

        public async Task<LogicResult<IEnumerable<TherapistBookingDto>>> GetTherapistByOutlet_Date_Service_TimeSlot(int outletId, DateTime date, int service, int timeSlot)
        {
            var unitOfWork = _repositoryHelper.GetUnitOfWork();
            var therapistRepo = _repositoryHelper.GetRepository<ITherapistRepository>(unitOfWork);
            var appointmentDetailRepo = _repositoryHelper.GetRepository<IAppoitmentDetailRepository>(unitOfWork);

            //get therapists already have booking
            var therapistsBusy = await therapistRepo.GetAsync(a => a.AppointmentDetails.Any(x => x.Bed1.Room1.Outlet == outletId
                                                                && x.Date == date
                                                                && x.timeSlot == timeSlot
                                                                && x.Service == service) && a.Possition == (int)Position.therapist);
            //find all therapists of this service
            var therapistOfServices = await therapistRepo.GetAsync(x => x.Staff_Services.Any(y => y.Service == service) && x.Possition == (int)Position.therapist);
            if (therapistOfServices == null)
            {
                return new LogicResult<IEnumerable<TherapistBookingDto>>() { IsSuccess = false, message = Validation.ServiceNotInOutlet, Result = null };
            }

            //find therapists free
            var therapistFrees = new List<TherapistBookingDto>();

            foreach (var therapist in therapistOfServices)
            {
                if (!therapistsBusy.Any(x => x.ID.Equals(therapist.ID)))
                {
                    var therapistFree = new TherapistBookingDto()
                    {
                        therapistID = therapist.ID,
                        therapistName = therapist.Name,
                        therapistAvatar = therapist.Avatar
                    };
                    therapistFrees.Add(therapistFree);
                }
            }
            //dont have any therapists free
            if (therapistFrees == null)
                return new LogicResult<IEnumerable<TherapistBookingDto>>() { IsSuccess = false, message = Validation.FileNotFound, Result = null };
            
            return new LogicResult<IEnumerable<TherapistBookingDto>>() { IsSuccess = true, Result = therapistFrees };
        }

        public async Task<LogicResult<IEnumerable<Booking_history_of_customersGroupDto>>> GetHistoryBookingCustomer(int IDCustomer, int dayPassed)
        {
            if (dayPassed < 0)
                return new LogicResult<IEnumerable<Booking_history_of_customersGroupDto>>() { IsSuccess = false, message = Validation.InvalidParameters, Result = null };
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var appointmentDetailRepo = _repositoryHelper.GetRepository<IAppoitmentDetailRepository>(unitofwork);
            var customerRepo = _repositoryHelper.GetRepository<ICustomerRepository>(unitofwork);
            var listAppointDetail = await appointmentDetailRepo.GetAppointmentIncludeServiceAndOutletForCustomer(IDCustomer, dayPassed);
            var result = new List<Booking_history_of_customersDto>();
            var customer = customerRepo.GetById(IDCustomer);
            if (customer == null)
                return new LogicResult<IEnumerable<Booking_history_of_customersGroupDto>>() { IsSuccess = false, message = Validation.CustomerIsNotInOutlet, Result = null };
            foreach (var item in listAppointDetail)
            {
                //get 1 booking_history
                var bookingDto = new Booking_history_of_customersDto()
                {
                    AppointmentId = item.Appointment1.ID,
                    status = item.Appointment1.Status,
                    dateServe = item.Date,
                    nameService = item.Service1.Name,
                    nameOutlet=item.Bed1.Room1.Outlet1.Name
                };
                result.Add(bookingDto);
            }
            List<Booking_history_of_customersGroupDto> lisFinalres = new List<Booking_history_of_customersGroupDto>();
            for (int i = 0; i < result.Count(); i++)
            {
                int kq1 = isExist(lisFinalres, result[i].AppointmentId);
                if (kq1 == -1)
                {
                    var finalreturn = new Booking_history_of_customersGroupDto()
                    {
                        AppointmentId = result[i].AppointmentId,
                        nameOutlet = result[i].nameOutlet,
                        dateServe = String.Format("{0:ddd, MMM d, yyyy}", result[i].dateServe),
                        timeServe = String.Format("{0:t}", result[i].dateServe),
                        status = Enum.GetName(typeof(BookingHistoryStatus), result[i].status),
                        listnameService = new List<string>()
                    };
                    finalreturn.listnameService.Add(result[i].nameService);
                    lisFinalres.Add(finalreturn);
                }
                else
                    lisFinalres[kq1].listnameService.Add(result[i].nameService);
            }
            return new LogicResult<IEnumerable<Booking_history_of_customersGroupDto>>()
            {
                IsSuccess = true,
                Result = lisFinalres

            };
        }

        public async Task<LogicResult<IEnumerable<CreateBookingDto>>> CreateBooking(IEnumerable<CreateBookingDto> bookings, int customerID, int outletID, string note)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var appointmentDetailRepo = _repositoryHelper.GetRepository<IAppoitmentDetailRepository>(unitofwork);
            var appointmentRepo = _repositoryHelper.GetRepository<IAppoitmentRepository>(unitofwork);
            var serviceRepo = _repositoryHelper.GetRepository<IServiceRepository>(unitofwork);
            var buffertimeRepo = _repositoryHelper.GetRepository<IBufferTimeRepository>(unitofwork);
            var bedRepo = _repositoryHelper.GetRepository<IBedRepository>(unitofwork);

            var bookingOks = new List<CreateBookingDto>();

            int appointmentID = 0; //appoinment != 0 when we have already appointment in DB

            foreach (var booking in bookings)
            {
                var therapistValid = await GetTherapistByOutlet_Date_Service_TimeSlot(outletID, booking.bookingDate, booking.serviceID, booking.timeSlotID);
                var timeslotValid = await GetTimeslotByOutlet_Date_Service_Therapist(outletID, booking.bookingDate, booking.serviceID, booking.therapistID);

                //check booking is valid
                if (therapistValid.IsSuccess && timeslotValid.IsSuccess && therapistValid.Result.Any(x => x.therapistID.Equals(booking.therapistID)) && 
                    timeslotValid.Result.Any(x => x.ID.Equals(booking.timeSlotID)) && (booking.bookingDate > DateTime.Now || (booking.bookingDate == DateTime.Now
                    && booking.timeSlotID > ((DateTime.Now.TimeOfDay - new TimeSpan(7,15,0)).TotalMinutes)/15)))
                {
                   using (DbContextTransaction transaction = unitofwork.Context.Database.BeginTransaction())
                    {
                        //-------------------------------------------------create appointment--------------------------------------------
                        if (appointmentID == 0)
                        {
                            var appointment = new Appointment()
                            {
                                bookingTime = DateTime.Now,
                                Status = (int)BookingHistoryStatus.Booked,
                                Customer = customerID,
                            };
                            appointmentRepo.Create(appointment);

                            var savechangeResult = await unitofwork.SaveChangesAsync();
                            if (!savechangeResult)
                            {
                                transaction.Rollback();
                                return new LogicResult<IEnumerable<CreateBookingDto>>() { IsSuccess = false, message = Validation.BookingFail };
                            }

                            var appointmentAlreadySave = await appointmentRepo.GetFirstAsync(null, x => x.OrderByDescending(y => y.ID));

                            //set appointmentID new value
                            appointmentID = appointmentAlreadySave.ID;
                        }
                                
                        //------------------------------------------------------------------------------------------------------------------
                        //-------------------------------------------------create appointment detail----------------------------------------
                        //------------------------------------------------------------------------------------------------------------------

                        //---------------------------get buffertime-------------------------------//
                        int customertype = 0;
                        
                        var apointmentHistory = await appointmentRepo.GetExistsAsync(x => x.Customer == customerID && x.AppointmentDetails.Any(y => y.Service == booking.serviceID));
                        if (!apointmentHistory)
                        {
                            var appointmentOrtherService = await appointmentRepo.GetExistsAsync(x => x.Customer == customerID);
                            if (!appointmentOrtherService)
                                customertype = (int)CustomerType.Fresh;
                            else
                                customertype = (int)CustomerType.FirstTime;
                        }
                        else
                            customertype = (int)CustomerType.Used;

                        var bufferTime = await buffertimeRepo.GetFirstAsync(x => x.Service == booking.serviceID && x.customerType == customertype);
                        if (bufferTime == null)
                        {
                            bufferTime = new BufferTime()
                            {
                                bufferTime1 = 0,
                                Service = booking.serviceID,
                                customerType = customertype,
                            };
                        }

                        //---------------------------get bed-------------------------------//
                        //get Bed is invisible in this time
                        var appointments = await appointmentDetailRepo.GetAsync(x => x.Date == booking.bookingDate && x.Service == booking.serviceID
                                                                    && x.Staff == booking.therapistID && x.timeSlot == booking.timeSlotID
                                                                    && x.Bed1.Room1.Outlet == outletID, null, "Bed1");

                        var bedInvisible = appointments.Select(x => x.Bed1);

                        //get all Bed can do this service
                        var bedService = await bedRepo.GetAsync(x => x.Service_Bed.Any(z => z.Service == booking.serviceID) && x.Room1.Outlet == outletID);

                        var bedOK = new Bed();
                        foreach (var bed in bedService)
                        {
                            if (!bedInvisible.Any(x => x.ID.Equals(bed.ID)))
                            {
                                bedOK = bed;
                                break;
                            }
                        }

                        if (bedOK != null)
                        {
                            //--------------------create appointment detail--------------------//
                            var appointmentDetail = new AppointmentDetail()
                            {
                                Bed = bedOK.ID,
                                BufferTime = bufferTime.bufferTime1,
                                Date = booking.bookingDate,
                                Appointment = appointmentID,
                                Staff = booking.therapistID,
                                Service = booking.serviceID,
                                Note = note,
                                timeSlot = booking.timeSlotID
                            };

                            appointmentDetailRepo.Create(appointmentDetail);

                            var resultSaveChange = await unitofwork.SaveChangesAsync();

                            //if cannot savechange and bookingOKs == null => rollback
                            if (!resultSaveChange && !bookingOks.Any())
                                transaction.Rollback();

                            //commit if oke savechange. If can not savechange, dont mark booking in bookingOKs list
                            if (resultSaveChange)
                            {
                                bookingOks.Add(booking);
                                transaction.Commit();
                            }
                        }
                        else
                        {
                            if (!bookingOks.Any())
                                transaction.Rollback();
                        }
                    }
                }
            }
            if (!bookingOks.Any())
                return new LogicResult<IEnumerable<CreateBookingDto>>() { IsSuccess = false, message = Validation.BookingSoldOut };

            return new LogicResult<IEnumerable<CreateBookingDto>>() { IsSuccess = true, Result = bookingOks };
        }

        public async Task<LogicResult<IEnumerable<Booking_history_of_customersGroupDto>>> GetHistoryBooking(int IDOutlet,int IDTherapist, int dayPassed, string nameCustomer)
        {
            if (dayPassed <= 0)
                return new LogicResult<IEnumerable<Booking_history_of_customersGroupDto>>() { IsSuccess = false, message = Validation.InvalidParameters, Result = null };
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var appointmentDetailRepo = _repositoryHelper.GetRepository<IAppoitmentDetailRepository>(unitofwork);
            var therapistRepo = _repositoryHelper.GetRepository<ITherapistRepository>(unitofwork);
            var outletRepo = _repositoryHelper.GetRepository<IOutletRepository>(unitofwork);

            var listAppointDetail = await appointmentDetailRepo.GetAppointmentIncludeServiceAndOutlet( IDOutlet,IDTherapist, dayPassed, nameCustomer);
            var result = new List<Booking_history_of_customersDto>();
            var therapist = therapistRepo.GetById(IDTherapist);

            if (therapist == null)
                return new LogicResult<IEnumerable<Booking_history_of_customersGroupDto>>() { IsSuccess = false, message = Validation.TherepistIsNotInOutlet, Result = null };

            var outlet = outletRepo.GetById(IDOutlet);
            if (outlet == null)
                return new LogicResult<IEnumerable<Booking_history_of_customersGroupDto>>() { IsSuccess = false, message = Validation.NoOutletWithID, Result = null };
            foreach (var item in listAppointDetail)
            {
                //TimeSpan span = DateTime.Now.Subtract(item.Date);
                //int stt = 0;
                //if (span.Hours < 2 && span.Hours > 0)
                //    stt = (int)BookingHistoryStatus.Performing;
                //else
                //    stt = (int)BookingHistoryStatus.Completed;


                //get 1 booking_history
                var bookingDto = new Booking_history_of_customersDto()
                {
                    AppointmentId = item.Appointment1.ID,
                    status = item.Appointment1.Status,
                    dateServe = item.Date,
                    nameService = item.Service1.Name,
                    nameCustomer = item.Appointment1.Customer1.Name,
                    nameOutlet=item.Bed1.Room1.Outlet1.Name
                    

                };
                result.Add(bookingDto);
            }
            List<Booking_history_of_customersGroupDto> lisFinalres = new List<Booking_history_of_customersGroupDto>();
            for (int i = 0; i < result.Count(); i++)
            {
                int kq1 = isExist(lisFinalres, result[i].AppointmentId);
                if (kq1 == -1)
                {
                    var finalreturn = new Booking_history_of_customersGroupDto()
                    {
                        AppointmentId = result[i].AppointmentId,
                        nameOutlet = result[i].nameOutlet,
                        dateServe = String.Format("{0:ddd, MMM d, yyyy}", result[i].dateServe),
                        timeServe = String.Format("{0:t}", result[i].dateServe),
                        status = Enum.GetName(typeof(BookingHistoryStatus), result[i].status),
                        nameCustomer = result[i].nameCustomer,
                        listnameService = new List<string>()
                    };
                    finalreturn.listnameService.Add(result[i].nameService);
                    lisFinalres.Add(finalreturn);
                }
                else
                    lisFinalres[kq1].listnameService.Add(result[i].nameService);
            }
            return new LogicResult<IEnumerable<Booking_history_of_customersGroupDto>>()
            {
                IsSuccess = true,
                Result = lisFinalres

            };
        }
        public async Task<LogicResult<IEnumerable<MeasurementCustomerDto>>> GetMeasurementByCustomer(int IDCustomer, int IDservice)
        {
            if (IDCustomer < 1)
                return new LogicResult<IEnumerable<MeasurementCustomerDto>>() { IsSuccess = false, message = Validation.InvalidParameters };
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var customerRepo = _repositoryHelper.GetRepository<ICustomerRepository>(unitofwork);
            var serviceRepo = _repositoryHelper.GetRepository<IServiceRepository>(unitofwork);
            var customer = customerRepo.GetById(IDCustomer);
            var service = serviceRepo.GetById(IDservice);
            if (customer == null)
                return new LogicResult<IEnumerable<MeasurementCustomerDto>>() { IsSuccess = false, message = Validation.CustomerNotExit, Result = null };
            if (service == null)
                return new LogicResult<IEnumerable<MeasurementCustomerDto>>() { IsSuccess = false, message = Validation.ServiceDoesnotexist, Result = null };

            var appoitmentDetailRepo = _repositoryHelper.GetRepository<IAppoitmentDetailRepository>(unitofwork);

            var LisMeasurement = await appoitmentDetailRepo.GetAppointmentIncludeTherapsitAndOutlet(IDCustomer, IDservice);
            var result = _mapper.Map<IEnumerable<MeasurementCustomerDto>>(LisMeasurement);

            return new LogicResult<IEnumerable<MeasurementCustomerDto>>() { IsSuccess = true, Result = result };

        }

        public async Task<LogicResult<IEnumerable<Booking_history_of_customersGroupDto>>> EditBooking(IEnumerable<EditBookingDto> bookings, int customerID, int outletID, string note)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var appointmentDetailRepo = _repositoryHelper.GetRepository<IAppoitmentDetailRepository>(unitofwork);
            var appointmentRepo = _repositoryHelper.GetRepository<IAppoitmentRepository>(unitofwork);
            var serviceRepo = _repositoryHelper.GetRepository<IServiceRepository>(unitofwork);
            var buffertimeRepo = _repositoryHelper.GetRepository<IBufferTimeRepository>(unitofwork);
            var bedRepo = _repositoryHelper.GetRepository<IBedRepository>(unitofwork);

            var appointmentID = bookings.First().appointmentID;

            //----------------------------cancel booking-----------------------------------
            var oldBookingHistorys = await appointmentDetailRepo.GetAsync(x => x.Appointment == appointmentID);
            foreach (var oldBookingHistory in oldBookingHistorys)
            {
                if (!bookings.Any(x => x.serviceID.Equals(oldBookingHistory.Service)))
                {
                    appointmentDetailRepo.Delete(oldBookingHistory);
                }
            }
            var valResult = await unitofwork.SaveChangesAsync();
            if (!valResult)
            {
                return new LogicResult<IEnumerable<Booking_history_of_customersGroupDto>> { IsSuccess = false };
            }

            //----------------------------edit booking--------------------------------------
            foreach (var booking in bookings)
            {
                var therapistValid = await GetTherapistByOutlet_Date_Service_TimeSlot(outletID, booking.bookingDate, booking.serviceID, booking.timeSlotID);
                var timeslotValid = await GetTimeslotByOutlet_Date_Service_Therapist(outletID, booking.bookingDate, booking.serviceID, booking.therapistID);

                //check booking is valid
                if (therapistValid.IsSuccess && timeslotValid.IsSuccess && therapistValid.Result.Any(x => x.therapistID.Equals(booking.therapistID)) &&
                    timeslotValid.Result.Any(x => x.ID.Equals(booking.timeSlotID)))
                {
                    //---------------------------get bed-------------------------------//
                    //get Bed is invisible in this time
                    var appointments = await appointmentDetailRepo.GetAsync(x => x.Date == booking.bookingDate && x.Service == booking.serviceID
                                                                && x.Staff == booking.therapistID && x.timeSlot == booking.timeSlotID
                                                                && x.Bed1.Room1.Outlet == outletID, null, "Bed1");

                    var bedInvisible = appointments.Select(x => x.Bed1);

                    //get all Bed can do this service
                    var bedService = await bedRepo.GetAsync(x => x.Service_Bed.Any(z => z.Service == booking.serviceID) && x.Room1.Outlet == outletID);

                    var bedOK = new Bed();
                    foreach (var bed in bedService)
                    {
                        if (!bedInvisible.Any(x => x.ID.Equals(bed.ID)))
                        {
                            bedOK = bed;
                            break;
                        }
                    }

                    if (bedOK != null)
                    {
                        //--------------------edit appointment detail--------------------//
                        var appointmentDetail = new AppointmentDetail()
                        {
                            Appointment = booking.appointmentID,
                            Bed = bedOK.ID,
                            Date = booking.bookingDate,
                            Staff = booking.therapistID,
                            Service = booking.serviceID,
                            Note = note,
                            timeSlot = booking.timeSlotID
                        };

                        var fiedtoUpdate = new string[]
                        {
                            nameof(AppointmentDetail.Bed),
                            nameof(AppointmentDetail.Date),
                            nameof(AppointmentDetail.Note),
                            nameof(AppointmentDetail.Staff),
                            nameof(AppointmentDetail.timeSlot)
                        };

                        appointmentDetailRepo.Update(appointmentDetail, fiedtoUpdate);
                        var updateResult = await unitofwork.SaveChangesAsync();
                        if (!updateResult)
                        {
                            return new LogicResult<IEnumerable<Booking_history_of_customersGroupDto>>() { IsSuccess = false };
                        }
                    } 
                }
            }

            var bookingHistory = await GetHistoryBookingCustomer(customerID, DEFAULT_DAY_PASSED);
            return new LogicResult<IEnumerable<Booking_history_of_customersGroupDto>>() { IsSuccess = true, Result = bookingHistory.Result };
        }

        public async Task<LogicResult> CancelBooking (int appointmentID)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var appointmentDetailRepo = _repositoryHelper.GetRepository<IAppoitmentDetailRepository>(unitofwork);
            var appointmentRepo = _repositoryHelper.GetRepository<IAppoitmentRepository>(unitofwork);

            //check appointment
            var isHasAppointment = await appointmentDetailRepo.GetExistsAsync(x => x.Appointment == appointmentID);
            if (!isHasAppointment)
            {
                return new LogicResult() { IsSuccess = false, message = Validation.InvalidParameters };
            }
            //check numOfDay booking is available
            var IsDayCancel = await appointmentDetailRepo.GetExistsAsync(x => x.Appointment == appointmentID && (DateTime.Now - x.Date).TotalDays >= NUM_OF_DAY_CANCEL);
            if (!IsDayCancel)
                return new LogicResult() { IsSuccess = false, message = Validation.DayCancelInValid };
            var appointmentToUpdate = new Appointment()
            {
                ID = appointmentID,
                Status = (int)BookingHistoryStatus.Canceled
            };
            appointmentRepo.Update(appointmentToUpdate, new string[] { nameof(Appointment.Status) });

            var valResult = await unitofwork.SaveChangesAsync();
            if (!valResult)
            {
                return new LogicResult() { IsSuccess = false, message = Validation.ServerError };
            }
            return new LogicResult() { IsSuccess = true };
        }

        public bool IsHasBed (int outletId, DateTime date, int serviceID, int timeSlot, int therapistID)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var BedRepo = _repositoryHelper.GetRepository<IBedRepository>(unitofwork);
            var appointmentRepo = _repositoryHelper.GetRepository<IAppoitmentDetailRepository>(unitofwork);

            //get Bed is invisible in this time
            var bedInvisible = appointmentRepo.Get(x => x.Date == date && x.Service == serviceID
                                                        && x.Staff == therapistID && x.timeSlot == timeSlot
                                                        && x.Bed1.Room1.Outlet == outletId).Select(x => x.Bed1);

            //get all Bed can do this service
            var bedService = BedRepo.Get(x => x.Service_Bed.Any(z => z.Service == serviceID) && x.Room1.Outlet == outletId);

            if (bedService.Count() == bedInvisible.Count())
                return false;
            return true;
        }

        private int isExist(List<Booking_history_of_customersGroupDto> lisbooking, int AppointmentID)
        {
            int i = 0;
            foreach (var item in lisbooking)
            {

                if (item.AppointmentId == AppointmentID)
                    return i;
                i++;
            }
            return -1;

        }
        private int isExist(List<BookingDetailDto> lisbooking, int AppointmentID)
        {
            int i = 0;
            foreach (var item in lisbooking)
            {

                if (item.idAppointment == AppointmentID)
                    return i;
                i++;
            }
            return -1;

        }

        public async Task<LogicResult> PutMeasurement(MeasurementCustomerDto measurementCDTO)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<IMeasurementRepository>(unitofwork);
            try
            {
                var _measurementCDTO = _mapper.Map<Measurement>(measurementCDTO);

                var fieldsToUpdate = new string[]
                {
                    nameof(MeasurementCustomerDto.IDBooking),
                    nameof(MeasurementCustomerDto.SessionDay),
                    nameof(MeasurementCustomerDto.Outletname),
                    nameof(MeasurementCustomerDto.TherapistName),
                    nameof(MeasurementCustomerDto.Image1),
                    nameof(MeasurementCustomerDto.Image2),
                    nameof(MeasurementCustomerDto.ImageBefore)
                };

                repo.Update(_measurementCDTO, fieldsToUpdate);
                var result = await unitofwork.SaveChangesAsync();

                if (!result)
                {
                    return new LogicResult() { IsSuccess = false, message = Validation.ServerError };
                }
                return new LogicResult() { IsSuccess = true };
            }
            catch
            {
                return new LogicResult() { IsSuccess = false, message = Validation.ServerError };
            }
        }
        public async Task<LogicResult<BookingDetailDto>> GetBookingAndServiceInfo(int appointmentID)
        {
            if (appointmentID <= 0)
                return new LogicResult<BookingDetailDto>() { IsSuccess = false, message = Validation.InvalidParameters };
            
            var unitowork = _repositoryHelper.GetUnitOfWork();
            var appointmentDetailRepo = _repositoryHelper.GetRepository<IAppoitmentDetailRepository>(unitowork);
            var appointmentRepo = _repositoryHelper.GetRepository<IAppoitmentRepository>(unitowork);
            var results = await appointmentDetailRepo.GetBookingDetailInluceServiceTherapist(appointmentID);
            List<ServiceAndTherapsitDto> listService = new List<ServiceAndTherapsitDto>();
           
            foreach (var item in results)
            {


                var infoService = new ServiceAndTherapsitDto()
                {
                    nameService = item.Service1.Name,
                    nameTherapist = item.Staff1.Name,
                    costService = item.Cost.GetValueOrDefault(0),
                    tiemeFrom = item.TimeSlot1.From.ToString(),
                    timeTo = (TimeSpan.FromMinutes((item.BufferTime + item.Service1.numOfTimeSlot) * 15)).Add(item.TimeSlot1.From.GetValueOrDefault()).ToString()
                };
                listService.Add(infoService);

            }
            var Result = new BookingDetailDto()
            {
                idAppointment = results[0].Appointment,
                outletName = results[0].Bed1.Room1.Outlet1.Name,
                cost = results[0].Cost.GetValueOrDefault(),
                dateBooking = String.Format("{0:ddd, MMM d, yyyy}", results[0].Date),
                timeBooking = String.Format("{0:t}", results[0].Date),
                statusBooking = Enum.GetName(typeof(BookingHistoryStatus), results[0].Appointment1.Status),
                infoService= new List<ServiceAndTherapsitDto>(listService)
            };

            return new LogicResult<BookingDetailDto>()
            {
                IsSuccess = true,
                Result = Result
            };



        }

        //public async Task<LogicResult<IEnumerable<AppointmentDetailDto>>> GetCustomerFeedback(int appointmentId,int serviceId)
        //{
        //    if (appointmentId <= 0)
        //        return new LogicResult<IEnumerable<AppointmentDetailDto>>() { IsSuccess = false, message = Validation.InvalidParameters, Result = null };

        //    if(serviceId <= 0)
        //        return new LogicResult<IEnumerable<AppointmentDetailDto>>() { IsSuccess = false, message = Validation.InvalidParameters, Result = null };

        //    var unitofwork = _repositoryHelper.GetUnitOfWork();
        //    var repo = _repositoryHelper.GetRepository<IAppoitmentDetailRepository>(unitofwork);

        //    var feedback = await repo.GetAsync(x => x.Appointment == appointmentId && x.Service == serviceId);



        //    var result = _mapper.Map<IEnumerable<AppointmentDetailDto>>(feedback);
        //    return new LogicResult<IEnumerable<AppointmentDetailDto>>() { IsSuccess = true, Result = result };

        //}

        public async Task<LogicResult<IEnumerable<AppointmentDetailDto>>> GetCustomerFeedback(int customerId, int serviceId)
        {
            if (customerId <= 0)
                return new LogicResult<IEnumerable<AppointmentDetailDto>>() { IsSuccess = false, message = Validation.InvalidParameters, Result = null };

            if (serviceId <= 0)
                return new LogicResult<IEnumerable<AppointmentDetailDto>>() { IsSuccess = false, message = Validation.InvalidParameters, Result = null };

            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<IAppoitmentDetailRepository>(unitofwork);
            var appointmentRepo = _repositoryHelper.GetRepository<IAppoitmentRepository>(unitofwork);
            
            var feedback = await repo.GetAsync(x => x.Appointment1.Customer == customerId && x.Service == serviceId);
            
            var result = _mapper.Map<IEnumerable<AppointmentDetailDto>>(feedback);
            return new LogicResult<IEnumerable<AppointmentDetailDto>>() { IsSuccess = true, Result = result };

        }

        public async Task<LogicResult> CreateCustomerFeedback(int appointmentId,int serviceId, string FeedBack)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var appointRepo = _repositoryHelper.GetRepository<IAppoitmentDetailRepository>(unitofwork);
            var serviceRepo = _repositoryHelper.GetRepository<IServiceRepository>(unitofwork);
            
            var appointcheck = appointRepo.GetExists(x => x.Appointment == appointmentId);
            if (!appointcheck)
                return new LogicResult() { IsSuccess = false, message = Validation.AppointmentNotExists };
            
            var servicecheck = serviceRepo.GetExists(x => x.ID == serviceId);
            if (!servicecheck)
                return new LogicResult() { IsSuccess = false, message = Validation.ServiceDoesnotexist };

            //get appointment thỏa điều kiện
            var appointment = await appointRepo.GetAsync(x => x.Appointment == appointmentId && x.Service == serviceId);
            var appointmentOK = new AppointmentDetail();
           
            foreach (var appoint in appointment)
            {
                appointmentOK = appoint;
                appointmentOK.feedBack = FeedBack;
            }

            var fiedtoUpdate = new string[] { nameof(AppointmentDetail.feedBack) };
            
            appointRepo.Update(appointmentOK, fiedtoUpdate);
            var updateResult = await unitofwork.SaveChangesAsync();
            if (!updateResult)
            {
                return new LogicResult() { IsSuccess = false };
            }
            return new LogicResult() { IsSuccess = true };
        }

        public async Task<LogicResult> ChangeStatus(int appointmentId, int status)
        {
            if (appointmentId >= 0 || status > (int)BookingHistoryStatus.Canceled || status < (int)BookingHistoryStatus.Booked)
                return new LogicResult() { IsSuccess = false, message = Validation.InvalidParameters };
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var appointmentRepo = _repositoryHelper.GetRepository<IAppoitmentRepository>(unitofwork);
            var appointmentDetailRepo = _repositoryHelper.GetRepository<IAppoitmentDetailRepository>(unitofwork);

            //check appointment
            var appointment = await appointmentRepo.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                return new LogicResult() { IsSuccess = false, message = Validation.InvalidParameters };
            }

            //check numOfDay booking is available
            if (status == (int)BookingHistoryStatus.Canceled)
            {
                var IsDayCancel = await appointmentDetailRepo.GetExistsAsync(x => x.Appointment == appointmentId && (DateTime.Now - x.Date).TotalDays >= NUM_OF_DAY_CANCEL);
                if (!IsDayCancel)
                    return new LogicResult() { IsSuccess = false, message = Validation.DayCancelInValid };
            }

            var appointmentToUpdate = new Appointment()
            {
                ID = appointmentId,
                Status = status
            };
            appointmentRepo.Update(appointmentToUpdate, new string[] { nameof(Appointment.Status) });

            var valResult = await unitofwork.SaveChangesAsync();
            if (!valResult)
            {
                return new LogicResult() { IsSuccess = false, message = Validation.ServerError };
            }
            return new LogicResult() { IsSuccess = true };
        }

        public async Task<LogicResult<BookingPhotoDTO>> GetCustomerPhoto(int appointmentID)
        {
            if (appointmentID <= 0)
                return new LogicResult<BookingPhotoDTO>() { IsSuccess = false, message = Validation.InvalidParameters };
            else
            {
                var unitowork = _repositoryHelper.GetUnitOfWork();
                var appointmentDetailRepo = _repositoryHelper.GetRepository<IAppoitmentDetailRepository>(unitowork);
                var appointmentRepo = _repositoryHelper.GetRepository<IAppoitmentRepository>(unitowork);
                var serviceRepo = _repositoryHelper.GetRepository<IServiceRepository>(unitowork);
                var customerRepo = _repositoryHelper.GetRepository<ICustomerRepository>(unitowork);

                var appointmentDetail = (await appointmentDetailRepo.GetAsync(x=>x.Appointment==appointmentID)).First();
                var appointment = (await appointmentRepo.GetAsync(x=>x.ID==appointmentID)).First();
                var result = new BookingPhotoDTO()
                {
                    AppointmentId = appointment.ID,
                    imageBefore = appointmentDetail.imageBefore,
                    imageAfter1 = appointmentDetail.imageAfter1,
                    imageAfter2 = appointmentDetail.imageAfter2,                    
            };
                return new LogicResult<BookingPhotoDTO>() { IsSuccess = true, Result = result };
            }
        }

        public async Task<LogicResult> AddNote(AppointmentNote appointmentNote)
        {
            try
            {
                var unitofwork = _repositoryHelper.GetUnitOfWork();
                var repo = _repositoryHelper.GetRepository<IAppoitmentDetailRepository>(unitofwork);

                var appoitmentDetail = await repo.GetAsync(x => x.Appointment == appointmentNote.idAppointmentDetail);

                var _appointmentDetail = appoitmentDetail.First();
                _appointmentDetail.Note = appointmentNote.note;

                repo.Update(_appointmentDetail, new string[] { nameof(AppointmentDetail.Note) });
                var result = await unitofwork.SaveChangesAsync();

                if (!result)
                {
                    return new LogicResult() { IsSuccess = false, message = Validation.ServerError };
                }
                return new LogicResult() { IsSuccess = true, message = "Add Success" };
            }
            catch
            {
                return new LogicResult() { IsSuccess = false, message = Validation.ServerError };
            }

        }

        public async Task<LogicResult> UpdateSignature(CustomerSignDTO customerSignDTO)
        {
            try
            {
                var unitofwork = _repositoryHelper.GetUnitOfWork();
                var repo = _repositoryHelper.GetRepository<IAppoitmentDetailRepository>(unitofwork);

                var appoitmentDetail = await repo.GetAsync(x => x.Appointment == customerSignDTO.idAppointmentDetail);

                var _appointmentDetail = appoitmentDetail.First();
                _appointmentDetail.CustomerSign = customerSignDTO.SignatureSign;

                repo.Update(_appointmentDetail, new string[] { nameof(AppointmentDetail.CustomerSign) });
                var result = await unitofwork.SaveChangesAsync();

                if (!result)
                {
                    return new LogicResult() { IsSuccess = false, message = Validation.ServerError };
                }
                return new LogicResult() { IsSuccess = true, message = "Sign Success" };
            }
            catch
            {
                return new LogicResult() { IsSuccess = false, message = Validation.ServerError };
            }

        }
    }
}
