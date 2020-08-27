using API.Model.Model;
using SPA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
namespace SPA.API.Mapping
{
    public class OutletMapping:Profile
    {
        public OutletMapping()
        {
            this.CreateMap<Outlet, OutletDetail>();
            this.CreateMap<ReviewOutlet, ReviewOutletDto>()
                .ForMember(dest => dest.CustomerId, x => x.MapFrom(src => src.Customer))
                .ForMember(dest => dest.CustomerName, x => x.MapFrom(src => src.Customer1.Name));
        }
    }
}