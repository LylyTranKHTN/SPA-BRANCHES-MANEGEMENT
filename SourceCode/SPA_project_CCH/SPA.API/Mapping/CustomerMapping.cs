using AutoMapper;

using SPA.Domain.Models;
using API.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Model.Enum;

namespace SPA.API.Mapping
{
    public class CustomerMapping:Profile
    {
        public CustomerMapping()
        {
            this.CreateMap<Customer, CustomerDetail>();
            this.CreateMap<CustomerDetail, Customer>();
            this.CreateMap<Customer, CustomerListDTO>().ForMember(dest => dest.avt, opt => opt.MapFrom(src => src.Avatar))
                                                       .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
                                                       .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
                                                       .ForMember(dest => dest.phonenumber, opt => opt.MapFrom(src => src.Phone))
                                                       .ForMember(dest => dest.nric, opt => opt.MapFrom(src => src.NRIC));
        }
    }
}