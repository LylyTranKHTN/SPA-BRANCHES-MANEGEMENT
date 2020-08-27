
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
    public interface ICustomerService
    {
        IRepositoryHelper _repositoryHelper { get; }

        /// <summary>
        /// Get customer profile by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List Customer</returns>
        Task<LogicResult<CustomerDetail>> GetCustomerById(int id);
        /// <summary>
        /// Get customer by outlet, therapist, date
        /// </summary>
        /// <param name="outlet"></param>
        /// <param name="therapist"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<LogicResult<IEnumerable<CustomerUpComming>>> GetCustomerByOutlet_Therapist_Date(int outlet, int therapist, DateTime date);

        /// <summary>
        /// Create and Save a new customer
        /// </summary>
        /// <param name="customer"></param>
        LogicResult PostCustomer(CustomerRegiter customer);

        /// <summary>
        /// Edit customer profile
        /// </summary>
        /// <param name="customer"></param>
        ///void PutCustomer(CustomerDetail customer);
       
        /// <summary>
        /// Edit customer profile
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<LogicResult> EditCustomer(CustomerProfileDetail customer);


        //void PutCustomer(CustomerDetail customer);

        //<summary>
        // Get profile of Customer
        //<para name = "id"></param>
        //<return>ProfileCustomer</return>
        Task<LogicResult<CustomerProfileDetail>> GetCustomerProfile(int id);

        Task<LogicResult<IEnumerable<CustomerListDTO>>> GetCustomerByOutletAndText(int outlet, string txt);
        Task<LogicResult<CustomerDetail>> GetCustomerbyUsernamePassword(string username, string password);
        //Task<LogicResult<BodyMeasurementCustomerDTO>> GetCustomerBodymeasurementById(int id);

        Task<LogicResult<IEnumerable<CustomerListDTO>>> GetAllCustomerByOutlet(int outlet);
        Task<int> CheckCustomerExisByEmailOrPhoneOrNRIC(string info);

    }
}
