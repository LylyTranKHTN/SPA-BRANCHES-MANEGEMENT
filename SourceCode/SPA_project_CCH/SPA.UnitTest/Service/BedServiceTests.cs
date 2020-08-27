
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
    public class BedServiceTests
    {
        public Mock<IRepositoryHelper> RepoHelperMock { get; set; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; set; }
        public Mock<IMapper> MapperMock { get; set; }
        public Mock<IBedRepository> BedRepoMock { get; set; }

        [TestInitialize]
        public void Setup()
        {
            RepoHelperMock = new Mock<IRepositoryHelper>();
            UnitOfWorkMock = new Mock<IUnitOfWork>();
            MapperMock = new Mock<IMapper>();
            BedRepoMock = new Mock<IBedRepository>();

            RepoHelperMock.Setup(p => p.GetUnitOfWork()).Returns(UnitOfWorkMock.Object);
            RepoHelperMock.Setup(p => p.GetRepository<IBedRepository>(It.IsAny<IUnitOfWork>())).Returns(BedRepoMock.Object);
        }
        [TestMethod]
        public async Task GetBedByOutletTest()
        {
            //arrange
            var listBeds = PrepareBeds();
            BedRepoMock.Setup(p => p.GetBedIncludeRoomByOutletID(It.IsAny<int>())).ReturnsAsync(listBeds);

            //act
            var service = new BedService(RepoHelperMock.Object, MapperMock.Object);
            var result = await service.GetBedByOutlet(1);

            //Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public async Task GetBedById_Success()
        {
            //arrange
            var Bed = PrepareBeds().First();
            BedRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(Bed);

            //act
            var service = new BedService(RepoHelperMock.Object, MapperMock.Object);
            var result = await service.GetBedById(1);

            //Assert
            Assert.IsTrue(result.IsSuccess);
           
        }

        [TestMethod]
        public async Task GetBedById_Failed_Id_InValid()
        {
            //arrange
            var Bed = PrepareBeds().First();
            BedRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(Bed);

            //act
            var service = new BedService(RepoHelperMock.Object, MapperMock.Object);
            var result = await service.GetBedById(0);

            //Assert
            Assert.IsFalse(result.IsSuccess);
        }

        private IEnumerable<Bed> PrepareBeds()
        {
            return new List<Bed>()
            {
                new Bed()
                {
                    ID = 1,
                    Room = 1,
                    Room1 = new Room()
                    {
                        ID = 1,
                        Name = "B101",
                        numOfBed = 3,
                    }
                },
                new Bed()
                {
                    ID = 2,
                    Room = 1,
                    Room1 = new Room()
                    {
                        ID = 1,
                        Name = "B101",
                        numOfBed = 3,
                    }
                }
            };
        }
    }
}