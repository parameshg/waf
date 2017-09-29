using Matrix.Firewall.Server.Services;
using Matrix.Firewall.Server.Services.Interfaces;
using SimpleInjector;

namespace Matrix.Firewall.Server
{
    public static class Dependency
    {
        public static void Register(Container container)
        {
            container.Register<ICountryService, CountryService>();
            container.Register<IPolicyService, PolicyService>();
        }
    }
}