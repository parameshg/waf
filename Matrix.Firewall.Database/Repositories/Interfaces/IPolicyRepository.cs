using System.Collections.Generic;

namespace Matrix.Firewall.Database.Repositories.Interfaces
{
    public interface IPolicyRepository
    {
        IEnumerable<Policy> GetPolicies();

        bool SavePolicy(string country, bool permission);

        bool PurgePolicy(string country);
    }
}