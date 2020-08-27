using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using Moq;
using SPA.BUS.Service;
using SPA.Domain.Models;
using SPA.Repository.Repository;
using SPA.Repository.UnitOfWork;
using System.Threading.Tasks;
using System.Linq;

namespace SPA.UnitTest.Service
{
    /// <summary>
    /// Summary description for TherapistServiceTests
    /// </summary>
    [TestClass]
    public class TherapistServiceTests
    {
        public Mock<IRepositoryHelper> RepoHelperMock { get; set; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; set; }
        public Mock<IMapper> MapperMock { get; set; }
        public Mock<ITherapistRepository> TherapistRepoMock { get; set; }

        [TestInitialize]
        public void TherapistInitialize()
        {
            RepoHelperMock = new Mock<IRepositoryHelper>();
            UnitOfWorkMock = new Mock<IUnitOfWork>();
            MapperMock = new Mock<IMapper>();
            TherapistRepoMock = new Mock<ITherapistRepository>();

            RepoHelperMock.Setup(p => p.GetUnitOfWork()).Returns(UnitOfWorkMock.Object);
            RepoHelperMock.Setup(p => p.GetRepository<ITherapistRepository>(It.IsAny<IUnitOfWork>())).Returns(TherapistRepoMock.Object);
        }


        [TestMethod]
        public async Task GetTherapistByIdTest()
        {
            //Arrange
            var therapistsData = new List<Staff>
            {
                new Staff {ID = 1, Name = "Minh", Possition = 3},
                new Staff {ID = 2, Name = "Ly", Possition = 3}
            };
            TherapistRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int i) => therapistsData.Single(t => t.ID == i));

            //Action
            var service = new TherapistService(RepoHelperMock.Object, MapperMock.Object);
            var result = await service.GetTherapistById(1);
            //Assert
            Assert.IsNotNull(result);
        }
    }
}
