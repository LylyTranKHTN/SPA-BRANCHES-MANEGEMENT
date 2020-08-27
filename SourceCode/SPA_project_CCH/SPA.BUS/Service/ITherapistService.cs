
using SPA.Domain.Models;
using API.Model;
using SPA.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Model.Model;

namespace SPA.BUS.Service
{
    public interface ITherapistService
    {
        IRepositoryHelper _repositoryHelper { get; }

        /// <summary>
        /// Get therapist profile by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List Therapist</returns>
        Task<LogicResult<TherapistProfile>> GetTherapistById(int id);

        /// <summary>
        /// Create and Save a new therapist
        /// </summary>
        /// <param name="therapist"></param>
        Task<LogicResult> PostTherapist(TherapistDetail therapist);

        /// <summary>
        /// Edit therapist profile
        /// </summary>
        /// <param name="therapist"></param>
        Task<LogicResult> PutTherapist(TherapistDetail therapist);

        /// <summary>
        /// Get therapist Profile where Therapis Name constain name
        /// </summary>
        /// <param name="name"></param>
        Task<LogicResult<IEnumerable<TherapistProfile>>> GetTherapistByName(string name);

        /// <summary>
        /// Get therapist by outlet ID
        /// </summary>
        /// <param name="outletID">Id outlet</param>
        /// <returns>List of Therapist? null</returns>
        Task<LogicResult<IEnumerable<TherapistProfile>>> GetTherapistByOutlet(int outletID);
        
        /// <summary>
        /// Get therapist by service ID
        /// </summary>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        Task<LogicResult<IEnumerable<TherapistProfile>>> GetTherapistByService(int serviceID);

        /// <summary>
        /// Get therapist by outlet ID and therapist name
        /// </summary>
        /// <param name="outletID"></param>
        /// <param name="TherapistName"></param>
        /// <returns></returns>
        Task<LogicResult<IEnumerable<TherapistProfile>>> GetTherapistByOutletAndTherapistName(int outletID, string TherapistName);

        Task<LogicResult<IEnumerable<TherapistListDTO>>> GetTherapistByOutletAndService(int outletID, int ServiceID);

        Task<LogicResult<TherapistDetail>> GetTherapistbyUsernamePassword(string username, string password);
        Task<int> CheckTherapistExisByEmailOrPhone(string info);
    }
}
