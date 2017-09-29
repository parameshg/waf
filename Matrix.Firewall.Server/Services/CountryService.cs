using Matrix.Firewall.Database.Repositories.Interfaces;
using Matrix.Firewall.Server.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Matrix.Firewall.Server.Services
{
    public class CountryService : ServiceBase, ICountryService
    {
        private ICountryRepository Countries { get; set; }

        private IRangeRepository Ranges { get; set; }

        public CountryService(ICountryRepository countries, IRangeRepository ranges)
        {
            Countries = countries;

            Ranges = ranges;
        }

        public IEnumerable<Country> GetCountries()
        {
            IEnumerable<Country> result = null;

            result = Countries.GetCountries();

            result = result.OrderBy(i => i.Name).ToList();

            return result;
        }

        public Country GetCountry(IPAddress address)
        {
            Country result = null;

            var ranges = Ranges.GetRangesUsingOctet1(int.Parse(address.ToString().Split('.')[0]));

            ranges = ranges.Where(i => new IPAddressRange(IPAddress.Parse(i.Range_From), IPAddress.Parse(i.Range_To)).IsInRange(address));

            var o = ranges.FirstOrDefault();

            if (o != null)
                result = Countries.GetCountries().Where(i => i.Code.Equals(o.Country)).FirstOrDefault();
            else
                result = Countries.GetCountries().Where(i => i.Code.Equals("XX")).FirstOrDefault();

            return result;
        }
    }
}