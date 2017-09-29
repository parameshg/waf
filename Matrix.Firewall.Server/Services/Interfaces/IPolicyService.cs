using System.Collections.Generic;
using System.Net;

namespace Matrix.Firewall.Server.Services.Interfaces
{
    public interface IPolicyService
    {
        bool Evaluate(IPAddress o);

        IEnumerable<Policy> GetPolicies();

        bool SavePolicy(string country, bool permission);

        bool PurgePolicy(string country);
    }
}