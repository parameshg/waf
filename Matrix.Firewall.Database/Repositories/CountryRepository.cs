using Matrix.Firewall.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Matrix.Firewall.Database.Repositories
{
    public class CountryRepository : RepositoryBase, ICountryRepository
    {
        public CountryRepository(string connection)
            : base(connection)
        {
        }

        public IEnumerable<Country> GetCountries()
        {
            IEnumerable<Country> result = null;

            result = Database.GetCollection<Country>(GetType().Name).FindAll().ToList();

            return result;
        }
    }
}