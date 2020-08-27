using Autofac;
using Autofac.Integration.WebApi;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace SPA.API.IoC
{
    public static class DependencyInjection
    {
        public static IContainer Register(IAppBuilder app, HttpConfiguration httpConfiguration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterWebApiTypes(httpConfiguration);
            builder.RegisterWebAppCustomModules();

            var container = builder.Build();

            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(httpConfiguration);

            return container;
        }

        private static void RegisterWebApiTypes(this ContainerBuilder builder, HttpConfiguration httpConfiguration)
        {
            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            // Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(httpConfiguration);
        }

        private static void RegisterWebAppCustomModules(this ContainerBuilder builder)
        {
            
            builder.RegisterModule<WebModule>();
            builder.RegisterModule<AutoMapperModule>();
        }
    }
}
