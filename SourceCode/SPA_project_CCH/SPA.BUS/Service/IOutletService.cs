
using SPA.Domain.Models;
using API.Model.Model;
using SPA.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.BUS.Service
{
    public interface IOutletService
    {
        IRepositoryHelper _repositoryHelper { get; }

        /// <summary>
        /// Get outlet profile by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List Outlet</returns>
        Task<LogicResult<OutletDetail>> GetOutletById(int id);

        /// <summary>
        /// Get outlet by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<LogicResult<IEnumerable<OutletDetail>>> GetOutletByName(string name);

        /// <summary>
        /// GetOutletReview
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<LogicResult<IEnumerable<ReviewOutletDto>>> GetOutletReview(int id);

        /// <summary>
        /// Create and Save a new outlet
        /// </summary>
        /// <param name="outlet"></param>
        // Task<LogicResult> PostOutlet(Outlet outlet);


        /// <summary>
        /// GetAllOutlet
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<LogicResult<IEnumerable<OutletDetail>>> GetAllOutlet();
        /// <summary>
        /// Edit outlet profile
        /// </summary>
        /// <param name="outlet"></param>
        Task<LogicResult> PutOutlet(OutletDetail outlet);

        /// <summary>
        /// CreateReviewOutlet
        /// </summary>
        /// <param name="outlet"></param>
        /// <returns></returns>
        LogicResult CreateReviewOutlet(int outletId, int customerid, int star, string content);

        /// <summary>
        /// BedAndTherapistNum
        /// </summary>
        /// <param name="outletID"></param>
        /// <returns></returns>
        Task<LogicResult<BedAndTherapistNum>> GetNumOfBedAndTherapistByOutlet(int outletID);

        /// <summary>
        /// GetAllOutletCA
        /// </summary>
        /// <returns></returns>
        /// CA: Customer App
        Task<LogicResult<IEnumerable<OutletDetailCA>>> GetAllOutletCA();

        /// <summary>
        /// GetOutletByNameCA
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<LogicResult<IEnumerable<OutletDetailCA>>> GetOutletByNameCA(string outletName);
    }
}
