using API.Model.Model;
using AutoMapper;
using SPA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SPA.API.Mapping
{
    public class BookingMapping:Profile
    {
        public BookingMapping()
        {
            this.CreateMap<TimeSlot, TimeSlotDTO>()
                .ForMember(d => d.From, o => o.MapFrom(s => s.From.ToString()))
                .ForMember(d => d.To, o => o.MapFrom(s => s.To.ToString()));
            this.CreateMap<ServiceAndTherapsitDto, ServiceAndTherapsitDto>()
                 .ForMember(d => d.costService, o => o.MapFrom(s => s.costService))
                 .ForMember(d => d.nameService, o => o.MapFrom(s => s.nameService))
                  .ForMember(d => d.nameTherapist, o => o.MapFrom(s => s.nameTherapist))
                  .ForMember(d => d.tiemeFrom, o => o.MapFrom(s => s.tiemeFrom))
                  .ForMember(d => d.timeTo, o => o.MapFrom(s => s.timeTo));
            //this.CreateMap<Appointment, AppointmentDetailDto>();
                 //.ForMember(d => d.Customer, o => o.MapFrom(s => s.Customer));
            this.CreateMap<AppointmentDetail, AppointmentDetailDto>();


        }
    }
}