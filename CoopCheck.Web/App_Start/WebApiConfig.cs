using System.Web.Http;
using log4net.Config;

namespace CoopCheck.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            XmlConfigurator.Configure();
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", 
                new {id = RouteParameter.Optional}
                );
        }
    }
}