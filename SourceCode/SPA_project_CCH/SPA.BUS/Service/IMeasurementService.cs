using API.Model.Model;
using SPA.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.BUS.Service
{
    public interface IMeasurementService
    { 
        IRepositoryHelper _repositoryHelper { get; }

        /// <summary>
        /// Get newest measurement for customer
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        Task<LogicResult<MeasurementDto>> GetMesurementNewest(int customerID);

        /// <summary>
        /// Get list measurement of booking history
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        Task<LogicResult<IEnumerable<MeasurementDto>>> GetMeasurementHistory(int customerID);
    }
}
