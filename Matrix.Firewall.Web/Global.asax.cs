using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Matrix.Firewall.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            WebApp.RegisterAllAreas();
            WebApp.RegisterGlobalFilters(GlobalFilters.Filters);
            WebApp.RegisterRoutes(RouteTable.Routes);
            WebApp.RegisterBundles(BundleTable.Bundles);
        }
    }
}