using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Cloudify.App_Start;

namespace Cloudify
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
