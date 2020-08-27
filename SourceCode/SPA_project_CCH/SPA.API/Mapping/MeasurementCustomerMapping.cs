using API.Model.Model;
using AutoMapper;
using SPA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPA.API.Mapping
{
    public class MeasurementCustomerMapping : Profile
    {
        public MeasurementCustomerMapping()
        {
            this.CreateMap<AppointmentDetail, MeasurementCustomerDto>()
                .ForMember(p => p.IDBooking, o => o.MapFrom(s => s.Appointment1.ID))
                .ForMember(p => p.SessionDay, o => o.MapFrom(s => String.Format("{0:ddd, MMM d, yyyy}", s.Date)))
                .ForMember(p => p.Outletname, o => o.MapFrom(s => s.Bed1.Room1.Outlet1.Name))
                .ForMember(p => p.TherapistName, o => o.MapFrom(s => s.Staff1.Name))
                .ForMember(p => p.Image1, o => o.MapFrom(s => s.imageAfter1))
                .ForMember(p => p.Image2, o => o.MapFrom(s => s.imageAfter2))
                .ForMember(p => p.ImageBefore, o => o.MapFrom(s => s.imageBefore));
        }
    }
}