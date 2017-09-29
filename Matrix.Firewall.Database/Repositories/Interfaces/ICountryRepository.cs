using System.Collections.Generic;

namespace Matrix.Firewall.Database.Repositories.Interfaces
{
    public interface ICountryRepository
    {
        IEnumerable<Country> GetCountries();
    }
}