using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Matrix.Firewall.Web
{
    public static class WebApp
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection o)
        {
            o.Add(new HandleErrorAttribute());
        }

        public static void RegisterBundles(BundleCollection bundles)
        {
            // jquery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/scripts/jquery-{version}.js", "~/scripts/jquery.validate*"));
            
            // modernizr
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/scripts/modernizr-*"));
            
            // bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/scripts/bootstrap.js", "~/scripts/respond.js"));
            bundles.Add(new StyleBundle("~/styles/bootstrap").Include("~/content/bootstrap.css"));
            
            // angular
            bundles.Add(new ScriptBundle("~/bundles/angular").Include("~/scripts/angular.min.js", "~/scripts/angular-route.min.js"));

            // toastr
            bundles.Add(new ScriptBundle("~/bundles/toastr").Include("~/scripts/toastr.min.js"));
            bundles.Add(new StyleBundle("~/styles/toastr").Include("~/content/toastr.min.css"));

            // site
            bundles.Add(new StyleBundle("~/styles/web").Include("~/content/site.css"));
        }

        public static void RegisterAllAreas()
        {
            AreaRegistration.RegisterAllAreas();
        }
    }
}