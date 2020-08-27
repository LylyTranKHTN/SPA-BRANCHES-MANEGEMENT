// New
using SPA.Domain.Models;
using API.Model;
using SPA.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Model.Model;

//<NEW>
namespace SPA.BUS.Service
{
    public interface IServiceService
    {
        IRepositoryHelper _repositoryHelper { get; }
        /// <summary>
        /// GetServiceByName
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>

        Task<LogicResult<IEnumerable<ServiceDetail>>> GetServiceByName(string name);

        /// <summary>
        /// GetServiceByOutlet
        /// </summary>
        /// <param name="outletID"></param>
        /// <returns></returns>
        Task<LogicResult<IEnumerable<ServiceDetail>>> GetServiceByOutlet(int outletID);

        /// <summary>
        /// GetAllServiceType
        /// </summary>
        /// <param name="></param>
        /// <returns></returns>
        Task<LogicResult<IEnumerable<ServiceTypeDto>>> GetAllServiceType();

        /// <summary>
        /// CreateReviewService
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        Task<LogicResult> CreateReviewService(int serviceId, int customerid, int star, string content);

        /// <summary>
        /// Get Service Review
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<LogicResult<IEnumerable<ReviewServiceDto>>> GetServiceReview(int id);

        /// <summary>
        /// getBufferTime
        /// </summary>
        /// <param name="therapist"></param>
        /// <returns></returns>
        Task<LogicResult<IEnumerable<BufferTimeDetail>>> getBufferTime(int therapist);

        /// <summary>
        /// GetServiceByID
        /// </summary>
        /// <param name="ServiceID"></param>
        /// <returns></returns>
   
        /// <summary>
        /// GetServiceBy4Filter
        /// </summary>
        /// <param name="_5Filter"></param>
        /// <returns></returns>
        Task<LogicResult<IEnumerable<ServiceDetail>>> GetServiceBy5Filter(_5FilterServiceList _5Filter);

        Task<LogicResult<ServiceDTO>> GetServiceByID(int ServiceID);
    }
}
