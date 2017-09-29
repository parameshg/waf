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
    public class PolicyController : ControllerBase
    {
        private IPolicyService Server { get; set; }

        public PolicyController(IPolicyService service)
        {
            Server = service;
        }

        // GET /ip/127.0.0.1/eval
        [HttpGet]
        [Route("address/{address}/eval")]
        public async Task<bool> Eval(string address)
        {
            return await Task.Run(() =>
            {
                var result = false;

                result = Server.Evaluate(IPAddress.Parse(address));

                return result;
            });
        }

        // GET /policies
        [HttpGet]
        [Route("policies")]
        public async Task<IEnumerable<Policy>> Get()
        {
            return await Task.Run(() =>
            {
                IEnumerable<Policy> result = null;

                result = Server.GetPolicies();

                return result;
            });
        }

        // POST /policy
        [HttpPost]
        [Route("policy")]
        public async Task<bool> Post(Policy o)
        {
            return await Task.Run(() =>
            {
                var result = false;

                result = Server.SavePolicy(o.Country, o.Permission);

                return result;
            });
        }

        // DELETE /policy
        [HttpDelete]
        [Route("policy/{country}")]
        public async Task<bool> Delete(string country)
        {
            return await Task.Run(() =>
            {
                var result = false;

                result = Server.PurgePolicy(country);

                return result;
            });
        }
    }
}