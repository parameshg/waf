using Matrix.Firewall.Database.Repositories.Interfaces;
using Matrix.Firewall.Server.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Matrix.Firewall.Server.Services
{
    public class PolicyService : ServiceBase, IPolicyService
    {
        private IPolicyRepository Policies { get; set; }

        private ICountryService Countries { get; set; }

        public PolicyService(IPolicyRepository policies, ICountryService countries)
        {
            Policies = policies;

            Countries = countries;
        }

        public bool Evaluate(IPAddress o)
        {
            var result = false;

            var country = Countries.GetCountry(o);

            if (country != null)
            {
                var policy = GetPolicies().Where(i => i.Country.Equals(country.Code)).FirstOrDefault();

                if (policy != null)
                    result = policy.Permission;
            }

            return result;
        }

        public IEnumerable<Policy> GetPolicies()
        {
            IEnumerable<Policy> result = null;

            result = Policies.GetPolicies();

            return result;
        }

        public bool SavePolicy(string country, bool permission)
        {
            var result = false;

            result = Policies.SavePolicy(country, permission);

            return result;
        }

        public bool PurgePolicy(string country)
        {
            return Policies.PurgePolicy(country);
        }
    }
}