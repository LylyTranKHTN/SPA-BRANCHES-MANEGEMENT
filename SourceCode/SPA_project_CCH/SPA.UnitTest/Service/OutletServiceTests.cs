
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
    public class OutletServiceTests
    {
        public Mock<IRepositoryHelper> RepoHelperMock { get; set; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; set; }
        public Mock<IMapper> MapperMock { get; set; }
        public Mock<IOutletRepository> OuletRepoMock { get; set; }

        [TestInitialize]
        public void Setup()
        {
            RepoHelperMock = new Mock<IRepositoryHelper>();
            UnitOfWorkMock = new Mock<IUnitOfWork>();
            MapperMock = new Mock<IMapper>();
            OuletRepoMock = new Mock<IOutletRepository>();

            RepoHelperMock.Setup(p => p.GetUnitOfWork()).Returns(UnitOfWorkMock.Object);
            RepoHelperMock.Setup(p => p.GetRepository<IOutletRepository>(It.IsAny<IUnitOfWork>())).Returns(OuletRepoMock.Object);
        }
        
        [TestMethod]
        public async Task GetOutletById_Success()
        {
            //arrange
            var Bed = PrepareOutlet().First();
            OuletRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(Bed);

            //act
            var service = new OutletService(RepoHelperMock.Object, MapperMock.Object);
            var result = await service.GetOutletById(1);

            //Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public async Task GetOutletById_Failed_Id_InValid()
        {
            //arrange
            var Bed = PrepareOutlet().First();
            OuletRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(Bed);

            //act
            var service = new BedService(RepoHelperMock.Object, MapperMock.Object);
            var result = await service.GetBedById(0);

            //Assert
            Assert.IsFalse(result.IsSuccess);
        }

        private IEnumerable<Outlet> PrepareOutlet()
        {
            return new List<Outlet>()
            {
                new Outlet()
                {
                    ID = 1,
                    address="NVC",
                    Image="flick",
                    Name="SPA CCH",
                    Phone="01234",
                    Start=5
                },
                new Outlet()
                {
                    ID = 2,
                    address="NTP",
                    Image="flick",
                    Name="SPA CCH2",
                    Phone="01235",
                    Start=5
                }
            };
        }
    }
}