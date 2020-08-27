using API.Model;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SPA.BUS.Service;
using SPA.Domain.Models;
using SPA.Repository.Repository;
using SPA.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SPA.UnitTest.Service
{
    [TestClass]
    public class AppointmentServiceTests
    {
        public Mock<IRepositoryHelper> RepoHelperMock { get; set; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; set; }
        public Mock<IMapper> MapperMock { get; set; }

        public Mock<IBedRepository> BedRepoMock { get; set; }
        public Mock<IAppoitmentDetailRepository> AppointmentDetailRepoMock { get; set; }
        public Mock<IAppoitmentRepository> AppointmentRepoMock { get; set; }
        public Mock<IServiceRepository> ServiceRepoMock { get; set; }
        public Mock<IBufferTimeRepository> BufferTimeRepoMock { get; set; }
        public Mock<ITherapistRepository> TherapistRepoMock { get; set; }

        [TestInitialize]
        public void Setup()
        {
            RepoHelperMock = new Mock<IRepositoryHelper>();
            UnitOfWorkMock = new Mock<IUnitOfWork>();
            MapperMock = new Mock<IMapper>();

            BedRepoMock = new Mock<IBedRepository>();
            AppointmentDetailRepoMock = new Mock<IAppoitmentDetailRepository>();
            AppointmentRepoMock = new Mock<IAppoitmentRepository>();
            ServiceRepoMock = new Mock<IServiceRepository>();
            BufferTimeRepoMock = new Mock<IBufferTimeRepository>();
            TherapistRepoMock = new Mock<ITherapistRepository>();

            RepoHelperMock.Setup(p => p.GetUnitOfWork()).Returns(UnitOfWorkMock.Object);
            RepoHelperMock.Setup(p => p.GetRepository<IBedRepository>(It.IsAny<IUnitOfWork>())).Returns(BedRepoMock.Object);
            RepoHelperMock.Setup(p => p.GetRepository<IAppoitmentDetailRepository>(It.IsAny<UnitOfWork>())).Returns(AppointmentDetailRepoMock.Object);
            RepoHelperMock.Setup(p => p.GetRepository<IAppoitmentRepository>(It.IsAny<IUnitOfWork>())).Returns(AppointmentRepoMock.Object);
            RepoHelperMock.Setup(p => p.GetRepository<IServiceRepository>(It.IsAny<IUnitOfWork>())).Returns(ServiceRepoMock.Object);
            RepoHelperMock.Setup(p => p.GetRepository<IBufferTimeRepository>(It.IsAny<IUnitOfWork>())).Returns(BufferTimeRepoMock.Object);
            RepoHelperMock.Setup(p => p.GetRepository<ITherapistRepository>(It.IsAny<IUnitOfWork>())).Returns(TherapistRepoMock.Object);

        }
        [TestMethod]
        public async Task GetBookingByOutletAndDate_IsSuccess_Test()
        {
            //arrange
            var appointmentBooked = prepareAppointmentDetails();
            var services = prepareServices();
            var therapists = prepareTherapists();

            AppointmentDetailRepoMock.Setup(p => p.GetAsync(It.IsAny<Expression<Func<AppointmentDetail, bool>>>(), null, null, null, null)).ReturnsAsync(appointmentBooked);
            ServiceRepoMock.Setup(p => p.GetAsync(It.IsAny<Expression<Func<Domain.Models.Service, bool>>>(), null, null, null, null)).ReturnsAsync(services);
            TherapistRepoMock.Setup(p => p.GetAsync(It.IsAny<Expression<Func<Staff, bool>>>(), null, null, null, null));
            BedRepoMock.Setup(p => p.Get(It.IsAny<Expression<Func<Bed, bool>>>(), null, null, null, null)).Returns(new List<Bed>() { new Bed() { ID = 1 }, new Bed() { ID = 2 } });
            AppointmentDetailRepoMock.Setup(p => p.Get(It.IsAny<Expression<Func<AppointmentDetail, bool>>>(), null, null, null, null)).Returns(appointmentBooked);

            //act
            var appointmentService = new AppointmentService(RepoHelperMock.Object, MapperMock.Object);
            var result = await appointmentService.GetBookingByOutletAndDate(1, new DateTime(2019,12,18));

            //Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public async Task GetBookingByOutletAndDate_IsFalse_DateInPast_Test()
        {
            //act
            var appointmentService = new AppointmentService(RepoHelperMock.Object, MapperMock.Object);
            var result = await appointmentService.GetBookingByOutletAndDate(1, new DateTime(2018, 12, 13));

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(Validation.DateTimeIsInPast, result.message);
        }

        [TestMethod]
        public async Task GetBookingByOutletAndDate_IsFalse_AllServiceSoldOut_Test()
        {
            //arrange
            var appointmentBooked = prepareAppointmentDetails();
            var services = prepareServices();
            var therapists = prepareTherapists();

            AppointmentDetailRepoMock.Setup(p => p.GetAsync(It.IsAny<Expression<Func<AppointmentDetail, bool>>>(), null, null, null, null)).ReturnsAsync(appointmentBooked);
            ServiceRepoMock.Setup(p => p.GetAsync(It.IsAny<Expression<Func<Domain.Models.Service, bool>>>(), null, null, null, null)).ReturnsAsync(services);
            TherapistRepoMock.Setup(p => p.GetAsync(It.IsAny<Expression<Func<Staff, bool>>>(), null, null, null, null));
            BedRepoMock.Setup(p => p.Get(It.IsAny<Expression<Func<Bed, bool>>>(), null, null, null, null)).Returns(new List<Bed>() { new Bed() { ID = 1 }, new Bed() { ID = 2 } });
            AppointmentDetailRepoMock.Setup(p => p.Get(It.IsAny<Expression<Func<AppointmentDetail, bool>>>(), null, null, null, null)).Returns(prepareAppointmentDetailSoldOut);

            //act
            var appointmentService = new AppointmentService(RepoHelperMock.Object, MapperMock.Object);
            var result = await appointmentService.GetBookingByOutletAndDate(1, new DateTime(2019, 12, 18));

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(Validation.AllServiceSoldOut, result.message);
        }

        [TestMethod]
        public async Task GetTherapistByOutlet_Date_Service_TimeSlot_IsSuccess_Test()
        {
            //arrange
            var therapistBusy = prepareTherapists();
            var allTherapistOfService = prepareAllTherapists();

            TherapistRepoMock.Setup(p => p.GetAsync((a => a.AppointmentDetails.Any(x => x.Bed1.Room1.Outlet == It.IsAny<int>()
                                                                && x.Date == It.IsAny<DateTime>() && x.timeSlot == It.IsAny<int>()
                                                                && x.Service == It.IsAny<int>())), null, null, null, null)).ReturnsAsync(therapistBusy);

            TherapistRepoMock.Setup(p => p.GetAsync(x => x.Staff_Services.Any(y => y.Service == It.IsAny<int>()), null, null, null, null)).ReturnsAsync(allTherapistOfService);

            //act
            var appointmentService = new AppointmentService(RepoHelperMock.Object, MapperMock.Object);
            var result = await appointmentService.GetTherapistByOutlet_Date_Service_TimeSlot(1, new DateTime(2019, 12, 18), 1, 1);

            //assert
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public async Task GetTherapistByOutlet_Date_Service_TimeSlot_IsFailed_FileNotFound_Test()
        {
            //arrange
            var therapistBusy = prepareTherapists();

            TherapistRepoMock.Setup(p => p.GetAsync((a => a.AppointmentDetails.Any(x => x.Bed1.Room1.Outlet == It.IsAny<int>()
                                                                && x.Date == It.IsAny<DateTime>()
                                                                && x.timeSlot == It.IsAny<int>()
                                                                && x.Service == It.IsAny<int>())), null, null, null, null)).ReturnsAsync(therapistBusy);

            TherapistRepoMock.Setup(p => p.GetAsync(x => x.Staff_Services.Any(y => y.Service == It.IsAny<int>()), null, null, null, null)).ReturnsAsync(therapistBusy);

            //act
            var appointmentService = new AppointmentService(RepoHelperMock.Object, MapperMock.Object);
            var result = await appointmentService.GetTherapistByOutlet_Date_Service_TimeSlot(1, new DateTime(2019, 12, 18), 1, 1);

            //assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(Validation.FileNotFound, result.message);
        }

        [TestMethod]
        public async Task GetTherapistByOutlet_Date_Service_TimeSlot_IsFailed_ServiceNotInOutlet_Test()
        {
            //arrange
            TherapistRepoMock.Setup(p => p.GetAsync((a => a.AppointmentDetails.Any(x => x.Bed1.Room1.Outlet == It.IsAny<int>()
                                                                && x.Date == It.IsAny<DateTime>()
                                                                && x.timeSlot == It.IsAny<int>()
                                                                && x.Service == It.IsAny<int>())), null, null, null, null)).ReturnsAsync(new List<Staff>());

            TherapistRepoMock.Setup(p => p.GetAsync(x => x.Staff_Services.Any(y => y.Service == It.IsAny<int>()), null, null, null, null)).ReturnsAsync(new List<Staff>());

            //act
            var appointmentService = new AppointmentService(RepoHelperMock.Object, MapperMock.Object);
            var result = await appointmentService.GetTherapistByOutlet_Date_Service_TimeSlot(1, new DateTime(2019, 12, 18), 1, 1);

            //assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(Validation.ServiceNotInOutlet, result.message);
        }

        private IEnumerable<AppointmentDetail> prepareAppointmentDetails()
        {
            return new List<AppointmentDetail>()
            {
                new AppointmentDetail()
                {
                    Appointment = 1,
                    Bed = 1,
                    BufferTime = 1,
                    Service = 1,
                    Date = new DateTime(2019, 12, 18),
                    timeSlot = 1,
                    Staff = 1,
                }
            };
        }

        private IEnumerable<AppointmentDetail> prepareAppointmentDetailSoldOut()
        {
            return new List<AppointmentDetail>()
            {
                new AppointmentDetail()
                {
                    Appointment = 1,
                    Bed = 1,
                    BufferTime = 1,
                    Service = 1,
                    Date = new DateTime(2019, 12, 18),
                    timeSlot = 1,
                    Staff = 1,
                },
                new AppointmentDetail()
                {
                    Appointment = 1,
                    Bed = 2,
                    BufferTime = 1,
                    Service = 1,
                    Date = new DateTime(2019, 12, 18),
                    timeSlot = 1,
                    Staff = 1,
                }
            };
        }

        private IEnumerable<Domain.Models.Service> prepareServices()
        {
            return new List<Domain.Models.Service>()
            {
                new Domain.Models.Service()
                {
                    ID = 1,
                },
                new Domain.Models.Service()
                {
                    ID = 2,
                }
            };
        }

        private IEnumerable<Staff> prepareTherapists()
        {
            return new List<Staff>()
            {
                new Staff()
                {
                    ID = 1,
                    Name = "ly",
                    Avatar = null,
                },
                new Staff()
                {
                    ID = 2,
                    Name = "bao",
                    Avatar = null,
                }
            };
        }

        private IEnumerable<TimeSlot> prepareTimeslots()
        {
            return new List<TimeSlot>()
            {
                new TimeSlot()
                {
                    ID = 1,
                    From = new TimeSpan(7,0,0),
                    To = new TimeSpan(7,15,0),
                },
                new TimeSlot()
                {
                    ID = 4,
                    From = new TimeSpan(7,45,0),
                    To = new TimeSpan(8,0,0),
                },
                new TimeSlot()
                {
                    ID = 7,
                    From = new TimeSpan(8,30,0),
                    To = new TimeSpan(8,45,0),
                },
                new TimeSlot()
                {
                    ID = 10,
                    From = new TimeSpan(9,15,0),
                    To = new TimeSpan(9,30,0),
                },
            };
        }

        private IEnumerable<Staff> prepareAllTherapists()
        {
            return new List<Staff>()
            {
                new Staff()
                {
                    ID = 3,
                    Name = "my",
                    Avatar = null,
                },
                new Staff()
                {
                    ID = 1,
                    Name = "ly",
                    Avatar = null,
                },
                new Staff()
                {
                    ID = 2,
                    Name = "bao",
                    Avatar = null,
                },
                new Staff()
                {
                    ID = 4,
                    Name = "mai",
                    Avatar = null,
                }
            };
        }
    }
}
