using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;
using log4net.Config;

[assembly: XmlConfigurator(Watch = true)]

namespace CoopCheck.Web
{
    public class WebApiApplication : HttpApplication
    {
        private static readonly ILog log =
            LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();
            ////GlobalConfiguration.Configure(WebApiConfig.Register);
            ////FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            ////RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            log.Info("Coopcheck.Web Started");
        }
    }
}