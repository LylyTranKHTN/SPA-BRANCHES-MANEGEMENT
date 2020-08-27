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
    public class RoomServiceTests
    {
        public Mock<IRepositoryHelper> RepoHelperMock { get; set; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; set; }
        public Mock<IMapper> MapperMock { get; set; }
        public Mock<IRoomRepository> RoomRepoMock { get; set; }

        [TestInitialize]
        public void Setup()
        {
            RepoHelperMock = new Mock<IRepositoryHelper>();
            UnitOfWorkMock = new Mock<IUnitOfWork>();
            MapperMock = new Mock<IMapper>();
            RoomRepoMock = new Mock<IRoomRepository>();

            RepoHelperMock.Setup(p => p.GetUnitOfWork()).Returns(UnitOfWorkMock.Object);
            RepoHelperMock.Setup(p => p.GetRepository<IRoomRepository>(It.IsAny<IUnitOfWork>())).Returns(RoomRepoMock.Object);
        }

        [TestMethod]
        public async Task GetRoomById_Success()
        {
            //arrange
            var room = new Room()
            {
                ID = 1,
                Name = "B001",
                numOfBed = 3,
                Outlet = 1,
            };
            RoomRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(room);

            //act
            var service = new RoomService(RepoHelperMock.Object, MapperMock.Object);
            var result = await service.GetRoomById(1);

            //Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public async Task GetRoomById_Fail_ID_InValid()
        {
            //arrange
            var room = new Room()
            {
                ID = 1,
                Name = "B001",
                numOfBed = 3,
                Outlet = 1,
            };
            RoomRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(room);

            //act
            var service = new RoomService(RepoHelperMock.Object, MapperMock.Object);
            var result = await service.GetRoomById(0);

            //Assert
            Assert.IsFalse(result.IsSuccess);
        }
    }
}
