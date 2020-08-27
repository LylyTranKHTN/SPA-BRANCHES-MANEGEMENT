using AutoMapper;
using SPA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Model.Model;
using API.Model.Enum;
using SPA.BUS.Service;

namespace SPA.API.Mapping
{
    public class StaffMapping : Profile
    {
        public StaffMapping()
        {
            this.CreateMap<Staff, TherapistDetail>();
            this.CreateMap<TherapistDetail, Staff>();

            this.CreateMap<Staff, TherapistProfile>().ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar))
                                                      .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                                                      .ForMember(dest => dest.DayOfBirth, opt => opt.MapFrom(src => src.DoB))
                                                      .ForMember(dest => dest.Decription, opt => opt.MapFrom(src => src.Description))
                                                      .ForMember(dest => dest.Experience, opt => opt.MapFrom(src => src.Experient))
                                                      .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                                                      .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                                                      .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone));
            this.CreateMap<TherapistListDTO, Staff>().ForMember(dest => dest.Possition, opt => opt.MapFrom(src => (int)Position.therapist));
            this.CreateMap<Staff, TherapistListDTO>().ForMember(dest => dest.avatar, opt => opt.MapFrom(src => src.Avatar))
                                                       .ForMember(dest => dest.TherapistName, opt => opt.MapFrom(src => src.Name));
        }
    }
}