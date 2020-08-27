
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
    public interface IBedService
    {
        IRepositoryHelper _repositoryHelper { get; }

        /// <summary>
        /// Get bed and list of service by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List Bed</returns>
        Task<LogicResult<BedServiceDto>> GetBedById(int id);

        /// <summary>
        /// Create and Save a new bed
        /// </summary>
        /// <param name="bed"></param>
        void PostBed(Bed bed);

        /// <summary>
        /// Edit bed profile
        /// </summary>
        /// <param name="bed"></param>
        void PutBed(Bed bed);

        Task<LogicResult<IEnumerable<ManageBedDto>>> GetBedByOutlet(int outletID);

    }
}
