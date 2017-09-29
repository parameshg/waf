using System.Collections.Generic;
using System.Net;

namespace Matrix.Firewall.Server.Services.Interfaces
{
    public interface ICountryService
    {
        IEnumerable<Country> GetCountries();

        Country GetCountry(IPAddress address);
    }
}