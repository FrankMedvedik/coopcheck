using System.Reflection;
using System.Web.Http;
using CoopCheck.Web;
using Hangfire;
using Microsoft.Owin;
using Ninject;
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
            var webApiConfiguration = new HttpConfiguration();
            app.UseHangfireDashboard();
            app.UseHangfireServer();
            app.UseNinjectMiddleware(CreateKernel);
            app.UseNinjectWebApi(webApiConfiguration);

        }
        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }
    }
}