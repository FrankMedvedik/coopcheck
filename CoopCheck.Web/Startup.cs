using CoopCheck.Web;
using Hangfire;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace CoopCheck.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration 
                .UseSqlServerStorage("CoopCheck");

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}