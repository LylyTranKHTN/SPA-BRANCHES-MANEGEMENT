using Autofac;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using SPA.API.App_Start;
using SPA.API.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Cors;
using System.Web.Http;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(Startup))]
namespace SPA.API.App_Start
{
    public partial class Startup
    {
        protected static IContainer Container { get; set; }

        public void Configuration(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();

            //app.UseCors(new CorsOptions
            //{
            //    PolicyProvider = new CorsPolicyProvider
            //    {
            //        PolicyResolver = CreateCorsPolicy
            //    }
            //});

            //Authenticate
            ConfigureAuth(app);

            //Set up IoC
            Container = DependencyInjection.Register(app, configuration);

            WebApiConfig.Register(configuration);
            app.UseWebApi(configuration);

        }

        //private async Task<CorsPolicy> CreateCorsPolicy(IOwinRequest request)
        //{
        //    return new CorsPolicy
        //    {
        //        AllowAnyHeader = true,
        //        AllowAnyMethod = true,
        //        SupportsCredentials = true
        //    };
        //}
    }
}