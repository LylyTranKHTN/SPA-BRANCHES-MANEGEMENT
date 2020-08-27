using AutoMapper;
using API.Model.Model;
using SPA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPA.API.Mapping
{
    public class BedMapping : Profile
    {
        public BedMapping()
        {
            this.CreateMap<Bed, ManageBedDto>().ForMember(dest => dest.BedID, opt => opt.MapFrom(src => src.ID))
                                               .ForMember(dest => dest.RoomID, opt => opt.MapFrom(src => src.Room))
                                               .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room1.Name));
        }
    }
}