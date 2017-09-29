using System.Collections.Generic;

namespace Matrix.Firewall.Database.Repositories.Interfaces
{
    public interface IRangeRepository
    {
        IEnumerable<Range> GetRangesUsingOctet1(int octet);

        bool CreateRange(string from, string to, string country);

        bool DeleteRange(string from, string to, string country);
    }
}