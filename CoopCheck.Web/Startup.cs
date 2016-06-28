using System.Web.Http;
using CoopCheck.Web;
using Hangfire;
using Microsoft.Owin;
using Owin;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using GlobalConfiguration = Hangfire.GlobalConfiguration;

[assembly: OwinStartup(typeof (Startup))]

namespace CoopCheck.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration
                .UseSqlServerStorage("CoopCheck");
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseNinjectMiddleware(() => NinjectConfig.CreateKernel.Value);
            app.UseNinjectWebApi(config);
            app.UseHangfireDashboard();
            app.UseHangfireServer();

        }
        
    }
}