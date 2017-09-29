using Newtonsoft.Json;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Matrix.Firewall.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration o)
        {
            #region Formatters

            o.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            o.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            o.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            #endregion

            #region Routes

            o.MapHttpAttributeRoutes();

            o.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            #endregion

            #region Dependency

            var container = new Container();
            container.Options.DefaultLifestyle = Lifestyle.Scoped;
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            Database.Dependency.Register(container);
            Server.Dependency.Register(container);

            container.Verify();

            o.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            #endregion

            o.EnableCors(new EnableCorsAttribute(origins: "*", headers: "*", methods: "*"));
        }
    }
}