using AutoMapper;
using API.Model;
using SPA.BUS.Service;
using API.Model.Model;
using SPA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.ComponentModel.DataAnnotations;

namespace SPA.API.Controllers
{
    [RoutePrefix("room")]
    [Authorize]
    public class RoomController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IRoomService _roomService;

        public RoomController(IMapper mapper, IRoomService roomSevice)
        {
            _mapper = mapper;
            _roomService = roomSevice;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetRoomByID(int id)
        {
            var message = CreateMessageData($"Room/{id}", new KeyValuePair<string, string>("RoomID", id.ToString()));
            var room = await _roomService.GetRoomById(id);
            if (!room.IsSuccess)
            {
                return CreateValidationErrorResponse(message, new ValidationResult(room.message));
            }
            if (room.Result == null)
            {
                return CreateNotFoundResponse( message, message: Validation.FileNotFound);
            }
            return CreateOkResponse(message, room.Result);
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult EditRoom(RoomDetail room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _roomService.PutRoom(room);
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateRoom(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _roomService.PostRoom(room);
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }

            return StatusCode(HttpStatusCode.Created);
        }
    }
}
