using Matrix.Firewall.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Matrix.Firewall.Database.Repositories
{
    public class PolicyRepository : RepositoryBase, IPolicyRepository
    {
        public PolicyRepository(string connection)
            : base(connection)
        {
        }

        public IEnumerable<Policy> GetPolicies()
        {
            IEnumerable<Policy> result = null;

            result = Database.GetCollection<Policy>(GetType().Name).FindAll().ToList();

            return result;
        }

        public bool SavePolicy(string country, bool permission)
        {
            var result = false;

            PurgePolicy(country);

            result = Database.GetCollection<Policy>(GetType().Name).Insert(new Policy() { Country = country, Permission = permission }) != null;

            return result;
        }

        public bool PurgePolicy(string country)
        {
            var result = false;

            result = Database.GetCollection<Policy>(GetType().Name).Delete(i => i.Country.Equals(country)) > 0;

            return result;
        }
    }
}