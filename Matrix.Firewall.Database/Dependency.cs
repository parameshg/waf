using Matrix.Firewall.Database.Repositories;
using Matrix.Firewall.Database.Repositories.Interfaces;
using SimpleInjector;
using System.Configuration;

namespace Matrix.Firewall.Database
{
    public static class Dependency
    {
        public static void Register(Container container)
        {
            var connection = ConfigurationManager.AppSettings["matrix.firewall.database"];

            container.Register<IRangeRepository>(() => { return new RangeRepository(connection); });
            container.Register<ICountryRepository>(() => { return new CountryRepository(connection); });
            container.Register<IPolicyRepository>(() => { return new PolicyRepository(connection); });
        }
    }
}