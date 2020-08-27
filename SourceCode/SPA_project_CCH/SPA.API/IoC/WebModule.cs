using Autofac;
using SPA.BUS.Service;
using SPA.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPA.API.IoC
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            //Repository
            builder.RegisterType<RepositoryHelper>().As<IRepositoryHelper>().InstancePerRequest();

            //Service
            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerRequest();
            builder.RegisterType<BedService>().As<IBedService>().InstancePerRequest();
            builder.RegisterType<RoomService>().As<IRoomService>().InstancePerRequest();
            builder.RegisterType<TherapistService>().As<ITherapistService>().InstancePerRequest();
            builder.RegisterType<OutletService>().As<IOutletService>().InstancePerRequest();
            builder.RegisterType<ServiceService>().As<IServiceService>().InstancePerRequest();
            builder.RegisterType<AppointmentService>().As<IAppointmentService>().InstancePerRequest();
            builder.RegisterType<MeasurementService>().As<IMeasurementService>().InstancePerRequest();

        }
    }
}