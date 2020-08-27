
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
using System.Text;
using System.Threading.Tasks;

namespace SPA.UnitTest.Service
{
    [TestClass]
    public class ServiceServiceTests
    {
        public Mock<IRepositoryHelper> RepoHelperMock { get; set; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; set; }
        public Mock<IMapper> MapperMock { get; set; }
        public Mock<IServiceRepository> ServiceRepoMock { get; set; }

        [TestInitialize]
        public void Setup()
        {
            RepoHelperMock = new Mock<IRepositoryHelper>();
            UnitOfWorkMock = new Mock<IUnitOfWork>();
            MapperMock = new Mock<IMapper>();
            ServiceRepoMock = new Mock<IServiceRepository>();

            RepoHelperMock.Setup(p => p.GetUnitOfWork()).Returns(UnitOfWorkMock.Object);
            RepoHelperMock.Setup(p => p.GetRepository<IServiceRepository>(It.IsAny<IUnitOfWork>())).Returns(ServiceRepoMock.Object);
        }
        [TestMethod]
        public void GetServiceByNameTest()
        {
            //arrange
            //var listServices = PrepareServices();
            //ServiceRepoMock.Setup(p => p.GetById(It.IsAny<int>())).ReturnsAsync(listServices);
            var serviceData = new List<SPA.Domain.Models.Service>
            {
                new SPA.Domain.Models.Service {ID = 1, Name = "Service1", numOfTimeSlot = 3},
                new SPA.Domain.Models.Service {ID = 2, Name = "Service2", numOfTimeSlot = 3}
            };
            ServiceRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int i) => serviceData.Single(t => t.ID == i));
            //act
            var service = new ServiceService(RepoHelperMock.Object, MapperMock.Object);
            var result = service.GetServiceByName("Service1");
            
            //Assert
            Assert.IsTrue(result.Result.IsSuccess);
           
            
        }

    }
}