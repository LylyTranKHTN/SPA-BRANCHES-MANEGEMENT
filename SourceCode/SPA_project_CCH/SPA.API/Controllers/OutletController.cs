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
    [RoutePrefix("outlet")]
    //[Authorize]
    public class OutletController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IOutletService _outletService;

        public OutletController(IMapper mapper, IOutletService outletSevice)
        {
            _mapper = mapper;
            _outletService = outletSevice;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetOutletByID(int id)
        {
            var message = CreateMessageData($"Outlet/{id}", new KeyValuePair<string, string>("outletID", id.ToString()));
            var outlet = await _outletService.GetOutletById(id);
            if(!outlet.IsSuccess)
            {
                return CreateValidationErrorResponse(message, new ValidationResult(outlet.message));
            }
            if(outlet.Result==null)
            {
                return CreateNotFoundResponse(message,  Validation.FileNotFound);
            }
            return CreateOkResponse(message, outlet.Result);
        }

        

        [HttpGet]
        [Route("review/{id}")]
        public async Task<HttpResponseMessage> GetOutletReview(int id)
        {
            var message = CreateMessageData($"Outlet/{id}", new KeyValuePair<string, string>("outletID", id.ToString()));

            var review = await _outletService.GetOutletReview(id);
            if (!review.IsSuccess)
                return CreateValidationErrorResponse(message, new ValidationResult(review.message));
           
            if (review.Result == null)
            {
                return CreateNotFoundResponse(message, Validation.FileNotFound);
            }
            return CreateOkResponse(message, review.Result);
        }

        //[HttpGet]
        //[Route("name/{OutletName}")]
        //public async Task<HttpResponseMessage> GetOutletByName(string OutletName)
        //{
        //    var messageData = CreateMessageData($"outlet/name/{OutletName}", new KeyValuePair<string, string>(nameof(OutletName), OutletName));

        //    var outlets = await _outletService.GetOutletByName(OutletName);

        //    return CreateOkResponse(messageData, outlets.Result);
        //}

        //[HttpGet]
        //[Route("all")]
        //public async Task<HttpResponseMessage> GetAllOutlet()
        //{
        //    var message = CreateMessageData($"Outlet");
        //    var outlet = await _outletService.GetAllOutlet();
        //    return CreateOkResponse(message, outlet.Result);
        //}

        [HttpPut]
        [Route("")]
        public async Task<HttpResponseMessage> EditOutlet(OutletDetail outlet)
        {
            var message = CreateMessageData($"outlet");
            if (!ModelState.IsValid)
            {
                return CreateValidationErrorResponse(message, new ValidationResult(Validation.InvalidParameters));
            }
            

            var result = await _outletService.PutOutlet(outlet);
            if (!result.IsSuccess)
            {
                return CreateValidationErrorResponse(message, new ValidationResult(result.message));
            }
            return CreateOkResponse(message, outlet);
        }

        [HttpPost]
        [Route("review")]
        public HttpResponseMessage CreateReviewOutlet(CreateReviewOutletDto createReviewOutlet)
        {
            var message = CreateMessageData($"outlet/review");
            if (!ModelState.IsValid)
                return CreateValidationErrorResponse(message, new ValidationResult(Validation.InvalidParameters));

            var result = _outletService.CreateReviewOutlet(createReviewOutlet.outletId, createReviewOutlet.customerid, createReviewOutlet.star, createReviewOutlet.content);
            if (!result.IsSuccess)
            {
                return CreateValidationErrorResponse(message, new ValidationResult(result.message));
            }
            return CreateOkResponse(message, createReviewOutlet);
        }

        [HttpGet]
        [Route("BedAndTherapistNum/{id}")]
        public async Task<HttpResponseMessage> GetNumOfBedAndTherapistByOutlet(int id)
        {
            var message = CreateMessageData($"BedAndTherapistNum/{id}", new KeyValuePair<string, string>("outletID", id.ToString()));

            var numBedTherapist = await _outletService.GetNumOfBedAndTherapistByOutlet(id);
            if (!numBedTherapist.IsSuccess)
                return CreateValidationErrorResponse(message, new ValidationResult(numBedTherapist.message));

            if (numBedTherapist.Result == null)
            {
                return CreateNotFoundResponse(message, Validation.FileNotFound);
            }
            return CreateOkResponse(message, numBedTherapist.Result);
        }

    [HttpGet]
    [Route("all/{pagesize=int?}/{pageNumber=int?}")]
    public async Task<HttpResponseMessage> GetAllOutlet(int? pageSize, int? pageNumber)
    {
        var message = CreateMessageData($"all", new KeyValuePair<string, string>[] {
                                                new KeyValuePair<string, string>("pageSize", pageSize.ToString()),
                                                new KeyValuePair<string, string>("pageNumber", pageNumber.ToString())
                                            });

        var outletList = await _outletService.GetAllOutletCA();
        if (!outletList.IsSuccess)
            return CreateValidationErrorResponse(message, new ValidationResult(outletList.message));

        if (outletList.Result == null)
        {
            return CreateNotFoundResponse(message, Validation.FileNotFound);
        }
        var numOfRecords = outletList.Result.Count();

        if (pageSize.HasValue && pageNumber.HasValue)
        {
            if (numOfRecords <= ((pageSize - 1) * (pageNumber - 1)))
            {
                return CreateBadRequestErrorResponse(message, Validation.InvalidPageIndex);
            }

            var result = outletList.Result.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);

            return CreateOkResponse(message, result, pageNumber, pageSize, outletList.Result.Count());
        }
        return CreateOkResponse(message, outletList.Result);
    }

    [HttpGet]
    [Route("name/{OutletName}/{pagesize=int?}/{pageNumber=int?}")]
    public async Task<HttpResponseMessage> GetOutletByName(string outletName, int? pageSize, int? pageNumber)
    {
        var message = CreateMessageData($"name/{outletName}", new KeyValuePair<string, string>[] {
                                                new KeyValuePair<string, string>("outletName", outletName),
                                                new KeyValuePair<string, string>("pageSize", pageSize.ToString()),
                                                new KeyValuePair<string, string>("pageNumber", pageNumber.ToString())
                                            });

        var outletList = await _outletService.GetOutletByNameCA(outletName);

        if (!outletList.IsSuccess)
            return CreateValidationErrorResponse(message, new ValidationResult(outletList.message));

        if (outletList.Result == null)
        {
            return CreateNotFoundResponse(message, Validation.FileNotFound);
        }

        var numOfRecords = outletList.Result.Count();

        if (pageSize.HasValue && pageNumber.HasValue)
        {
            if (numOfRecords <= ((pageSize - 1) * (pageNumber - 1)))
            {
                return CreateBadRequestErrorResponse(message, Validation.InvalidPageIndex);
            }

            var result = outletList.Result.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);

            return CreateOkResponse(message, result, pageNumber, pageSize, outletList.Result.Count());
        }
        return CreateOkResponse(message, outletList.Result);
    }
}
}
