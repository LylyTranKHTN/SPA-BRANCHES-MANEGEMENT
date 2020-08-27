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
using API.Model.HangdingCodeModel;
using System.ComponentModel.DataAnnotations;
using API.Model;

namespace SPA.API.Controllers
{
    [RoutePrefix("bed")]
    [Authorize]
    public class BedController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IBedService _bedService;

        public BedController(IMapper mapper, IBedService bedSevice)
        {
            _mapper = mapper;
            _bedService = bedSevice;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetBedByID(int id)
        {
            var messege = CreateMessageData($"bed/{id}", new KeyValuePair<string, string>("bedID", id.ToString()));
            var bed = await _bedService.GetBedById(id);
            if (!bed.IsSuccess)
            {
                return CreateValidationErrorResponse(messege, new ValidationResult(bed.message));
            }
            if(bed.Result == null)
            {
                return CreateNotFoundResponse(messege, Validation.FileNotFound);
            }
            return CreateOkResponse(messege, bed.Result);
        }

        [HttpGet]
        [Route("outlet/{outletID}")]
        public async Task<HttpResponseMessage> GetBedByOutlet(int outletID)
        {
            var messageData = CreateMessageData($"bed/outlet/{outletID}",
                                    new KeyValuePair<string, string>("OutletId", outletID.ToString()));
            if (outletID < 1)
            {
                return CreateValidationErrorResponse(messageData, new ValidationResult(Validation.InvalidParameters));
            }

            var beds = await _bedService.GetBedByOutlet(outletID);

            return CreateOkResponse(messageData, beds.Result);
        }


        [HttpPut]
        [Route("")]
        public IHttpActionResult EditBed(Bed bed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _bedService.PutBed(bed);
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateBed(Bed bed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _bedService.PostBed(bed);
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }

            return StatusCode(HttpStatusCode.Created);
        }
    }
}
