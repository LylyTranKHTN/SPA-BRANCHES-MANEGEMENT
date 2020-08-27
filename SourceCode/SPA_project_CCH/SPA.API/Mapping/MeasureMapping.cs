using AutoMapper;
using API.Model.Model;
using SPA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Model.Enum;

namespace SPA.API.Mapping
{
    public class MeasureMapping : Profile
    {
        public MeasureMapping()
        {
            this.CreateMap<AppointmentDetail, MeasurementCustomerDto>().ForMember(dest => dest.IDBooking, opt => opt.MapFrom(src => src.Appointment))
                                               .ForMember(dest => dest.Image1, opt => opt.MapFrom(src => src.imageAfter1))
                                                .ForMember(dest => dest.Image2, opt => opt.MapFrom(src => src.imageAfter2))
                                                 .ForMember(dest => dest.ImageBefore, opt => opt.MapFrom(src => src.imageBefore))
                                                  .ForMember(dest => dest.Outletname, opt => opt.MapFrom(src => src.Bed1.Room1.Outlet1.Name))
                                                   .ForMember(dest => dest.SessionDay, opt => opt.MapFrom(src => src.Date))
                                                    .ForMember(dest => dest.TherapistName, opt => opt.MapFrom(src => src.Staff1.Name));


            this.CreateMap<Measurement, MeasurementDto>()
                .ForMember(d => d.colorBMI, o => o.MapFrom(s => (s.BMI > (double)BodyMeasurement.BMImax || s.BMI < (double)BodyMeasurement.BMImin) ? Color.needImprove : Color.normal))
                .ForMember(d => d.colorFatContent, o => o.MapFrom(s => (s.fatContent > (double)BodyMeasurement.FatContentmax || s.fatContent < (double)BodyMeasurement.FatContentmin) ? Color.needImprove : Color.normal))
                .ForMember(d => d.colorBodyMass, o => o.MapFrom(s => (s.bodyMass > (double)BodyMeasurement.BodyMassmax || s.bodyMass < (double)BodyMeasurement.BodyMassmin) ? Color.needImprove : Color.normal))
                .ForMember(d => d.colorMuscleContent, o => o.MapFrom(s => (s.muscleContent > (double)BodyMeasurement.MuscleContentmax || s.muscleContent < (double)BodyMeasurement.MuscleContentmin) ? Color.needImprove : Color.normal))
                .ForMember(d => d.colorWeight, o => o.MapFrom(s => (s.Weight > (double)BodyMeasurement.Weightmax || s.Weight < (double)BodyMeasurement.Weightmin) ? Color.needImprove : Color.normal))
                .ForMember(d => d.date, o => o.MapFrom(s => s.AppointmentDetail.Date))
                .ForMember(d => d.Height, o => o.MapFrom(s => Math.Round(s.Height, 3)))
                .ForMember(d => d.Weight, o => o.MapFrom(s => Math.Round(s.Weight, 3)))
                .ForMember(d => d.BMI, o => o.MapFrom(s => Math.Round(s.BMI, 3)))
                .ForMember(d => d.bodyMass, o => o.MapFrom(s => Math.Round(s.bodyMass, 3)))
                .ForMember(d => d.fatContent, o => o.MapFrom(s => Math.Round(s.fatContent, 3)))
                .ForMember(d => d.muscleContent, o => o.MapFrom(s => Math.Round(s.muscleContent, 3)));
        }
    }
}