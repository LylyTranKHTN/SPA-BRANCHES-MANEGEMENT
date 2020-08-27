using AutoMapper;
using API.Model.Model;
using SPA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPA.API.Mapping
{
    public class RoomMapping : Profile
    {
        public RoomMapping()
        {
            this.CreateMap<Room, RoomDetail>();
            this.CreateMap<RoomDetail, Room>();
        }
    }
}