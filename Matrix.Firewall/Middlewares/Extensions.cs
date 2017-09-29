using Owin;

namespace Matrix.Firewall.Middlewares
{
    public static class Extensions
    {
        public static void UseFirewall(this IAppBuilder app)
        {
            app.Use<WebRequestFirewall>();
        }
    }
}