using SPA.API.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;

namespace SPA.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //enable cors
            string origin = System.Configuration.ConfigurationManager.AppSettings["CorsOrigin"];
            string headers = System.Configuration.ConfigurationManager.AppSettings["CorsHeaders"];
            string methods = System.Configuration.ConfigurationManager.AppSettings["CorsMethods"];
            var cors = new EnableCorsAttribute(origin, headers, methods);
            config.EnableCors(cors);

            //Error handler
            config.Services.Replace(typeof(IExceptionHandler), new ExceptionErrorHandler());
        }
    }
}
