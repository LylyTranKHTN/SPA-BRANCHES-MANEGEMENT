using AutoMapper;
using API.Model.Model;
using SPA.BUS.Service;

using SPA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.ComponentModel.DataAnnotations;
using API.Model;

namespace SPA.API.Controllers
{
    [RoutePrefix("customer")]
    public class CustomerController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        private readonly IMeasurementService _measurementService;

        public CustomerController(IMapper mapper,ICustomerService customerSevice, IMeasurementService measurementService)
        {
            _mapper = mapper;
            _customerService = customerSevice;
            _measurementService = measurementService;
        }

        [HttpGet]
        [Route("{id}")]
        //[Authorize]
        public async Task<CustomerDetail> GetCustomerByID(int id)
        {
            var customer = await _customerService.GetCustomerById(id);
            return customer.Result;
        }

        [HttpGet]
        [Route("profile/{id}")]
        //[Authorize]
        public async Task<HttpResponseMessage> GetCustomerProfile(int id)
        {
            var message = CreateMessageData($"customer/profile/{id}", new KeyValuePair<string, string>("customerID", id.ToString()));
            var customer = await _customerService.GetCustomerProfile(id);
            if (!customer.IsSuccess)
            {
                return CreateValidationErrorResponse(message, new ValidationResult(customer.message));
            }
            if (customer.Result == null)
            {
                return CreateNotFoundResponse(message, Validation.FileNotFound);
            }
            return CreateOkResponse(message, customer.Result);

        }
        //[HttpGet]
        //[Route("bodymeasurement/{id}")]
        ////[Authorize]
        //public async Task<HttpResponseMessage> GetCustomerBodymeasurement(int id)
        //{
        //    var message = CreateMessageData($"customer/bodymeasurement/{id}", new KeyValuePair<string, string>("customerID", id.ToString()));
        //    var customer = await _customerService.GetCustomerBodymeasurementById(id);
        //    if (!customer.IsSuccess)
        //    {
        //        return CreateValidationErrorResponse(message, new ValidationResult(customer.message));
        //    }
        //    if (customer.Result == null)
        //    {
        //        return CreateNotFoundResponse(message, Validation.FileNotFound);
        //    }
        //    return CreateOkResponse(message, customer.Result);

        //}
        [HttpPost]
        [Route("profile")]
        public async Task<HttpResponseMessage> GetCustomerProfileByUsernamePassword(User user)
        {
            var message = CreateMessageData($"customer/profile/");

            var customer = await _customerService.GetCustomerbyUsernamePassword(user.UserName, user.Password);
            if (!customer.IsSuccess)
            {
                return CreateValidationErrorResponse(message, new ValidationResult(customer.message));
            }
            if (customer.Result == null)
            {
                return CreateNotFoundResponse(message, Validation.FileNotFound);
            }
            return CreateOkResponse(message, customer.Result);

        }

        [HttpGet]
        //[Authorize]
        [Route("up-comming/{outlet}/{therapist}/{date}")]
        public async Task<HttpResponseMessage> GetCustomerByOutlet_Therapist_Date(int outlet, int therapist, DateTime date)
        {
            var message = CreateMessageData($"customer/up-comming/{outlet}/{therapist}/{date}", new KeyValuePair<string, string>[] {
                                new KeyValuePair<string, string>("outletID",outlet.ToString()),
                                new KeyValuePair<string, string>("therapistID", therapist.ToString()),
                                new KeyValuePair<string, string>("date", date.ToString())});

            var customers = await _customerService.GetCustomerByOutlet_Therapist_Date(outlet, therapist, date);

            if (!customers.IsSuccess)
            {
                return CreateValidationErrorResponse(message, new ValidationResult(customers.message));
            }
            if (customers.Result == null)
            {
                return CreateNotFoundResponse(message, Validation.FileNotFound);
            }
            return CreateOkResponse(message, customers.Result);
        }

        [HttpPut]
        [Route("")]
        //[Authorize]
        public async Task<HttpResponseMessage> EditCustomer(CustomerProfileDetail customer)
        {
            var messageData = CreateMessageData($"customer");
            if (!ModelState.IsValid)
            {
                return CreateValidationErrorResponse(messageData, new ValidationResult(Validation.InvalidParameters));
            }
            var result = await _customerService.EditCustomer(customer);
            if (!result.IsSuccess)
            {
                return CreateValidationErrorResponse(messageData, new ValidationResult(result.message));
            }
            return CreateOkResponse(messageData, customer);
        }


        [HttpPost]
        [Route("")]
        public HttpResponseMessage PostCustomer(CustomerRegiter customer)
        {
            var message = CreateMessageData($"customer");

            if (!ModelState.IsValid)
                return CreateBadRequestErrorResponse(message, Validation.InvalidParameters);

            var postResult = _customerService.PostCustomer(customer);

            if (!postResult.IsSuccess)
                return CreateValidationErrorResponse(message, new ValidationResult(postResult.message));

            return CreateOkResponse(message, customer.Name);    
        }

        [HttpGet]
        [Route("outlet/{outletID}/txt/{txt}")]
        //[Authorize]
        public async Task<HttpResponseMessage> GetCustomerByOutletAndText(int outletID, string txt)
        {
            var message = CreateMessageData($"customer/outlet/{outletID}/txt/{txt}", new KeyValuePair<string, string>[] {
                                new KeyValuePair<string, string>("outletID",outletID.ToString()),
                                new KeyValuePair<string, string>("txt", txt) });

            var customers = await _customerService.GetCustomerByOutletAndText(outletID, txt);

            if (!customers.IsSuccess)
            {
                return CreateValidationErrorResponse(message, new ValidationResult(customers.message));
            }
            
            return CreateOkResponse(message, customers.Result);
        }

        [HttpGet]
        [Route("measurement/{customerID}")]
        public async Task<HttpResponseMessage> GetMeasurementNewest (int customerID)
        {
            var message = CreateMessageData($"customer/measurement/{customerID}", new KeyValuePair<string, string>("customerID", customerID.ToString()));

            var measurement = await _measurementService.GetMesurementNewest(customerID);
            if (!measurement.IsSuccess)
                return CreateValidationErrorResponse(message, new ValidationResult(measurement.message));

            return CreateOkResponse(message, measurement.Result);
        }

        [HttpGet]
        [Route("measurement/history/{customerID}/{pagesize=int?}/{pageNumber=int?}")]
        public async Task<HttpResponseMessage> GetMeasurementHistory (int customerID, int? pageSize, int? pageNumber)
        {
            var message = CreateMessageData($"customer/measurement/history/{customerID}/{pageSize}/{pageNumber}", new KeyValuePair<string, string>[] {
                                                new KeyValuePair<string, string>("customerID", customerID.ToString()),
                                                new KeyValuePair<string, string>("pageSize", pageSize.ToString()),
                                                new KeyValuePair<string, string>("pageNumber", pageNumber.ToString())
                                            });

            var measurement = await _measurementService.GetMeasurementHistory(customerID);
            if (!measurement.IsSuccess)
                return CreateValidationErrorResponse(message, new ValidationResult(measurement.message));

            var numOfRecords = measurement.Result.Count();

            if (pageSize.HasValue && pageNumber.HasValue)
            {
                if (numOfRecords <= ((pageSize - 1) * (pageNumber - 1)))
                {
                    return CreateBadRequestErrorResponse(message, Validation.InvalidPageIndex);
                }

                var result = measurement.Result.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);

                return CreateOkResponse(message, result, pageNumber, pageSize, measurement.Result.Count());
            }

            return CreateOkResponse(message, measurement.Result);
        }

        [HttpGet]
        [Route("outlet/{outletID}")]
        //[Authorize]
        public async Task<HttpResponseMessage> GetAllCustomerByOutlet(int outletID)
        {
            var message = CreateMessageData($"customer/outlet/{outletID}", new KeyValuePair<string, string>[] {
                                new KeyValuePair<string, string>("outletID",outletID.ToString()) }) ;

            var customers = await _customerService.GetAllCustomerByOutlet(outletID);

            if (!customers.IsSuccess)
            {
                return CreateValidationErrorResponse(message, new ValidationResult(customers.message));
            }

            return CreateOkResponse(message, customers.Result);
        }
        [HttpGet]
        [Route("isexist")]
        public async Task<HttpResponseMessage> checkCustomerExist(String info)
        {
            var message = CreateMessageData($"customer/isexist?info={info}", new KeyValuePair<string, string>("email", info));
            var isexist = await _customerService.CheckCustomerExisByEmailOrPhoneOrNRIC(info);
            return CreateOkResponse(message, isexist);
        }

    }
}
