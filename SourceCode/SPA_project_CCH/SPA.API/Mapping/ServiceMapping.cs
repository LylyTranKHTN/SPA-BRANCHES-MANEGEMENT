using API.Model.Model;
using AutoMapper;
using SPA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPA.API.Mapping
{
    public class ServiceMapping:Profile
    {
        public ServiceMapping()
        {
            this.CreateMap<Service, ServiceDetail>();
            this.CreateMap<Service, ServiceDTO>().ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                                                     .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                                                     .ForMember(dest => dest.NumOfTimeSlot, opt => opt.MapFrom(src => src.numOfTimeSlot))
                                                     .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                                                     .ForMember(dest => dest.Star, opt => opt.MapFrom(src => src.Star));
            this.CreateMap<Service, ServiceTypeDto>().ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                                                     .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            this.CreateMap<ReviewService, ReviewServiceDto>()
                .ForMember(dest => dest.CustomerId, x => x.MapFrom(src => src.Customer))
                .ForMember(dest => dest.CustomerName, x => x.MapFrom(src => src.Customer1.Name));
        }
    }
}