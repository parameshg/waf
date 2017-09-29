using Matrix.Firewall.Api.Filters;
using Matrix.Firewall.Server.Services.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Matrix.Firewall.Api.Controllers
{
    [LogApi]
    [LogError]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CountryController : ControllerBase
    {
        private ICountryService Server { get; set; }

        public CountryController(ICountryService service)
        {
            Server = service;
        }

        // GET /countries
        [HttpGet]
        [Route("countries")]
        public async Task<IEnumerable<Country>> Get()
        {
            return await Task.Run(() => 
            {
                IEnumerable<Country> result = null;

                result = Server.GetCountries();

                return result;
            });
        }

        // GET /address/127.0.0.1/country
        [HttpGet]
        [Route("address/{address}/country")]
        public async Task<Country> Get(string address)
        {
            return await Task.Run(() =>
            {
                Country result = null;

                result = Server.GetCountry(IPAddress.Parse(address));

                return result;
            });
        }
    }
}