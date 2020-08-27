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

namespace SPA.API.Controllers
{
    [RoutePrefix("therapist")]
    //[Authorize]
    public class TherapistController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ITherapistService _therapistService;

        public TherapistController(IMapper mapper, ITherapistService therapistSevice)
        {
            _mapper = mapper;
            _therapistService = therapistSevice;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetTherapistByID(int id)
        {
            var messageData = CreateMessageData($"therapist/{id}", new KeyValuePair<string, string>("TherapistID", id.ToString()));
            if (id <= 0)
            {
                CreateValidationErrorResponse(messageData, new ValidationResult(Validation.InvalidParameters));
            }
            var result = await _therapistService.GetTherapistById(id);
            if(result.Result == null)
            {
                return CreateNotFoundResponse(messageData, Validation.FileNotFound);
            }

            return CreateOkResponse(messageData, result.Result);
        }

        [HttpGet]
        [Route("name/{TherapistName}")]
        public async Task<HttpResponseMessage> GetTherapistByName(string TherapistName)
        {
            var messageData = CreateMessageData($"therapist/name/{TherapistName}", new KeyValuePair<string, string>(nameof(TherapistName), TherapistName));

            if (TherapistName == "")
            {
                return CreateValidationErrorResponse(messageData, new ValidationResult(Validation.InvalidParameters));
            }

            var therapists = await _therapistService.GetTherapistByName(TherapistName);
            
            return CreateOkResponse(messageData, therapists.Result);
        }

        [HttpGet]
        [Route("profile/{username}/{password}")]
        public async Task<HttpResponseMessage> GetTherapistProfileByUsernamePassword(string username, string password)
        {
            var message = CreateMessageData($"therapist/profile/{username}/{password}", new KeyValuePair<string, string>[]
                { new KeyValuePair<string, string>("username", username), new KeyValuePair<string, string>("password", password)});

            var therapist = await _therapistService.GetTherapistbyUsernamePassword(username, password);
            if (!therapist.IsSuccess)
            {
                return CreateValidationErrorResponse(message, new ValidationResult(therapist.message));
            }
            if (therapist.Result == null)
            {
                return CreateNotFoundResponse(message, Validation.FileNotFound);
            }
            return CreateOkResponse(message, therapist.Result);
        }

        [HttpGet]
        [Route("outlet/name/{outletID}/{TherapistName}")]
        public async Task<HttpResponseMessage> GetTherapistByOutletAndTherapistName(int outletID, string TherapistName)
        {
            var messageData = CreateMessageData($"therapist/outlet/name/{outletID}/{TherapistName}", new KeyValuePair<string, string>[] { new 
                            KeyValuePair<string, string>(nameof(outletID),outletID.ToString()), new KeyValuePair<string, string>(nameof(TherapistName),TherapistName) });

            if (outletID < 1 || TherapistName == "")
            {
                return CreateValidationErrorResponse(messageData, new ValidationResult(Validation.InvalidParameters));
            }

            var therapists = await _therapistService.GetTherapistByOutletAndTherapistName(outletID, TherapistName);

            return CreateOkResponse(messageData, therapists.Result);
        }

        [HttpGet]
        [Route("outlet/{outletID}/{pagesize=int?}/{pageNumber=int?}")]
        public async Task<HttpResponseMessage> GetTherapistByOutlet(int outletID, int? pageSize, int? pageNumber)
        {
            var messageData = CreateMessageData($"therapist/outlet/{outletID}", 
                                    new KeyValuePair<string, string>("OutletId", outletID.ToString()));
            if (outletID < 1)
            {
                return CreateValidationErrorResponse(messageData, new ValidationResult(Validation.InvalidParameters));
            }

            var therapists = await _therapistService.GetTherapistByOutlet(outletID);
            var numOfRecords = therapists.Result.Count();

            if (pageSize.HasValue && pageNumber.HasValue)
            {
                if (numOfRecords <= ((pageSize - 1) * (pageNumber - 1)))
                {
                    return CreateBadRequestErrorResponse(messageData, Validation.InvalidPageIndex);
                }

                var result = therapists.Result.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);

                return CreateOkResponse(messageData, result, pageNumber, pageSize, therapists.Result.Count());
            }

            return CreateOkResponse(messageData, therapists.Result);
        }

        [HttpGet]
        [Route("service/{serviceID}")]
        public async Task<HttpResponseMessage> GetTherapistByService(int serviceID)
        {
            var messageData = CreateMessageData($"therapist/service/{serviceID}", new KeyValuePair<string, string>("serviceID", serviceID.ToString()));

            if(serviceID < 1)
            {
                return CreateValidationErrorResponse(messageData, new ValidationResult(Validation.InvalidParameters));
            }

            var therapists = await _therapistService.GetTherapistByService(serviceID);

            return CreateOkResponse(messageData, therapists.Result);
        }

        [HttpPut]
        [Route("")]
        public async Task<HttpResponseMessage> EditTherapist(TherapistDetail therapist)
        {
            var messageData = CreateMessageData($"therapist");
            if (!ModelState.IsValid)
            {
                return CreateValidationErrorResponse(messageData, new ValidationResult(Validation.InvalidParameters));
            }

            var result = await _therapistService.PutTherapist(therapist);
            if (!result.IsSuccess)
            {
                return CreateSystemErrorResponse(messageData);
            }

            //if(result.message != null)
            //{
            //    return CreateValidationErrorResponse(messageData, new ValidationResult(result.message));
            //}
            return CreateOkResponse(messageData, therapist);
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> CreateTherapist(TherapistDetail therapist)
        {
            var messageData = CreateMessageData($"therapist");

            if (!ModelState.IsValid)
            {
                return CreateValidationErrorResponse(messageData, new ValidationResult(Validation.InvalidParameters));
            }

            var result = await _therapistService.PostTherapist(therapist);
            if (!result.IsSuccess)
            {
                return CreateSystemErrorResponse(messageData);
            }

            return CreateOkResponse(messageData, therapist);
        }

        [HttpGet]
        [Route("service/{serviceID}/outlet/{outletID}")]
        public async Task<HttpResponseMessage> GetTherapistByOutletAndService(int outletID, int ServiceID)
        {
            var messageData = CreateMessageData($"therapist/service/{ServiceID}/outlet/{outletID}", new KeyValuePair<string, string>[] { new
                            KeyValuePair<string, string>(nameof(ServiceID),ServiceID.ToString()), new KeyValuePair<string, string>(nameof(outletID),outletID.ToString()) });
            if (outletID < 1)
            {
                return CreateValidationErrorResponse(messageData, new ValidationResult(Validation.InvalidParameters));
            }

            if (ServiceID < 1)
            {
                return CreateValidationErrorResponse(messageData, new ValidationResult(Validation.InvalidParameters));
            }

            var therapists = await _therapistService.GetTherapistByOutletAndService(outletID, ServiceID);

            return CreateOkResponse(messageData, therapists.Result);
        }

        [HttpGet]
        [Route("isexist")]
        public async Task<HttpResponseMessage> checkTherapsitExist(String info)
        {
            var message = CreateMessageData($"therapist/isexist?info={info}", new KeyValuePair<string, string>("info", info));
            var isexist = await _therapistService.CheckTherapistExisByEmailOrPhone(info);
            return CreateOkResponse(message, isexist);
        }



    }
}
