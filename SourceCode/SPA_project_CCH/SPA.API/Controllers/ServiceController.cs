using AutoMapper;
using API.Model;
using SPA.BUS.Service;
using SPA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using API.Model.Model;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio.Exceptions;

namespace SPA.API.Controllers
{
    [RoutePrefix("service")]
   // [Authorize]
    public class ServiceController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IServiceService _serviceService;

        public ServiceController(IMapper mapper, IServiceService serviceSevice)
        {
            _mapper = mapper;
            _serviceService = serviceSevice;
        }

        [HttpGet]
        [Route("name/{ServiceName}")]
        public async Task<HttpResponseMessage> GetServiceByName(string ServiceName)
        {
            var messageData = CreateMessageData($"service/name/{ServiceName}", new KeyValuePair<string, string>(nameof(ServiceName), ServiceName));

            if (ServiceName=="")
            {
                return CreateValidationErrorResponse(messageData, new ValidationResult(Validation.InvalidParameters));
            }

            var services = await _serviceService.GetServiceByName(ServiceName);
            
            return CreateOkResponse(messageData, services.Result);
        }

        [HttpGet]
        [Route("outlet/{outletID}/{pagesize=int?}/{pageNumber=int?}")]
        public async Task<HttpResponseMessage> GetServiceByOutlet(int outletID, int? pageSize, int? pageNumber)
        {
            var messageData = CreateMessageData($"service/outlet/{outletID}", new KeyValuePair<string, string>[] {
                                                new KeyValuePair<string, string>("OuletId", outletID.ToString()),
                                                new KeyValuePair<string, string>("pageSize", pageSize.ToString()),
                                                new KeyValuePair<string, string>("pageNumber", pageNumber.ToString())
                                            });

            if (outletID <1)
                return CreateValidationErrorResponse(messageData, new ValidationResult(Validation.InvalidParameters));

            var services = await _serviceService.GetServiceByOutlet(outletID);
            var numOfRecords = services.Result.Count();

            if (pageSize.HasValue && pageNumber.HasValue)
            {
                if (numOfRecords <= ((pageSize - 1) * (pageNumber - 1)))
                {
                    return CreateBadRequestErrorResponse(messageData, Validation.InvalidPageIndex);
                }

                var listService = services.Result.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);

                return CreateOkResponse(messageData, listService, pageNumber, pageSize, services.Result.Count());
            }

            return CreateOkResponse(messageData, services.Result);
        }

        [HttpGet]
        [Route("buffertime/{therapistID}")]
        public async Task<HttpResponseMessage> getBufferTime(int therapistID)
        {
            var messageData = CreateMessageData($"service/buffertime/{therapistID}",
                                new KeyValuePair<string, string>("therapist", therapistID.ToString()));

            if (therapistID < 1)
            {
                return CreateValidationErrorResponse(messageData,
                                                     new ValidationResult(Validation.InvalidParameters));
            }

            var buffertimes = await _serviceService.getBufferTime(therapistID);

            return CreateOkResponse(messageData, buffertimes.Result);
        }

        [HttpGet]
        [Route("alltype")]
        public async Task<HttpResponseMessage> GetAllServiceType()
        {
            var message = CreateMessageData($"alltype");

            var servicetype = await _serviceService.GetAllServiceType();
            if (!servicetype.IsSuccess)
                return CreateValidationErrorResponse(message, new ValidationResult(servicetype.message));

            if (servicetype.Result == null)
            {
                return CreateNotFoundResponse(message, Validation.FileNotFound);
            }
            return CreateOkResponse(message, servicetype.Result);
        }

        [HttpGet]
        [Route("review/{id}")]
        public async Task<HttpResponseMessage> GetServiceReview(int id)
        {
            var message = CreateMessageData($"service/review/{id}", new KeyValuePair<string, string>("serviceID", id.ToString()));

            var review = await _serviceService.GetServiceReview(id);
            if (!review.IsSuccess)
                return CreateValidationErrorResponse(message, new ValidationResult(review.message));

            if (review.Result == null)
            {
                return CreateNotFoundResponse(message, Validation.FileNotFound);
            }
            return CreateOkResponse(message, review.Result);
        }

        [HttpPost]
        [Route("review")]
        public async Task<HttpResponseMessage> CreateReviewService(CreateReviewServiceDto createReviewService)
        {
            var message = CreateMessageData($"service/review");
            if (!ModelState.IsValid)
                return CreateValidationErrorResponse(message, new ValidationResult(Validation.InvalidParameters));

            var result = await _serviceService.CreateReviewService(createReviewService.serviceId, createReviewService.customerid, createReviewService.star, createReviewService.content);
            if (!result.IsSuccess)
            {
                return CreateValidationErrorResponse(message, new ValidationResult(result.message));
            }
            return CreateOkResponse(message, createReviewService);
        }


        [HttpGet]
        [Route("id/{ServiceID}")]
        public async Task<HttpResponseMessage> GetServiceByID(int ServiceID)
        {
            var messageData = CreateMessageData($"service/id/{ServiceID}",
                                          new KeyValuePair<string, string>("ServiceId", ServiceID.ToString()));
            if (ServiceID < 1 )
            {
                return CreateValidationErrorResponse(messageData, new ValidationResult(Validation.InvalidParameters));
            }

            var services = await _serviceService.GetServiceByID(ServiceID);

            return CreateOkResponse(messageData, services.Result);
        }

        [HttpPost]
        [Route("GetServiceBy5Filter/{pagesize=int?}/{pageNumber=int?}")]
        public async Task<HttpResponseMessage> GetServiceBy5Filter(_5FilterServiceList _5Filter, int? pageSize, int? pageNumber)
        {
            var messageData = CreateMessageData($"service/GetServiceBy5Filter");

            var services = await _serviceService.GetServiceBy5Filter(_5Filter);

            if (!services.IsSuccess)
            {
                return CreateSystemErrorResponse(messageData);
            }

            var numOfRecords = services.Result.Count();

            if (pageSize.HasValue && pageNumber.HasValue)
            {
                if (numOfRecords <= ((pageSize - 1) * (pageNumber - 1)))
                {
                    return CreateBadRequestErrorResponse(messageData, Validation.InvalidPageIndex);
                }

                var listService = services.Result.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);

                return CreateOkResponse(messageData, listService, pageNumber, pageSize, services.Result.Count());
            }

            return CreateOkResponse(messageData, services.Result);
        }
        [HttpGet]
        [Route("verycode/{phone}/{msg}")]
        public HttpResponseMessage SendSMSVerycode(string phone, string msg)
        {
            var message = CreateMessageData($"service/verycode/{phone}/{msg}");
            var accountSid = "AC4f9d7812a1eeb2e8d5e1b7008f38dff7";
            var authToken = "a460fe4c4fde4ee44fb7b327dd1efb72";
            TwilioClient.Init(accountSid, authToken);
            var messageOptions = new CreateMessageOptions(new PhoneNumber(("+84"+phone).ToString()));
            messageOptions.From = new PhoneNumber("+16623378292");
            messageOptions.Body = msg;
            try
            {
                var result = MessageResource.Create(messageOptions);
                
                //Console.WriteLine(result.Body);
                return CreateOkResponse(message, "Đã gửi 1 mã xác thức tới số máy "+ phone);
            }
            catch (TwilioException e)
            {
                Console.WriteLine(e);
                if (e.ToString().Contains("The number  is unverified") || e.ToString().Contains("number  is not a valid phone number"))
                    return CreateOkResponse(message, "Số điện thoại không đúng");
                else
                    return CreateOkResponse(message, "server error");
            }
        }

    }
}
