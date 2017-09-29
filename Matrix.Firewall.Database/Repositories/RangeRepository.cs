using Matrix.Firewall.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Net;

namespace Matrix.Firewall.Database.Repositories
{
    public class RangeRepository : RepositoryBase, IRangeRepository
    {
        public RangeRepository(string connection)
            : base(connection)
        {
        }

        public IEnumerable<Range> GetRangesUsingOctet1(int octet)
        {
            IEnumerable<Range> result = null;

            result = Database.GetCollection<Range>(GetType().Name).Find(i => i.Range_From_Octet_1 >= octet - 1 && i.Range_From_Octet_1 <= octet + 1);

            return result;
        }

        public bool CreateRange(string from, string to, string country)
        {
            var result = false;

            var from_args = from.Split('.');

            var to_args = to.Split('.');

            result = Database.GetCollection<Range>(GetType().Name).Insert(new Range()
            {
                Range_From = from,
                Range_From_Value = IPAddress.Parse(from).Address,
                Range_From_Octet_1 = int.Parse(from_args[0]),
                Range_From_Octet_2 = int.Parse(from_args[1]),
                Range_From_Octet_3 = int.Parse(from_args[2]),
                Range_From_Octet_4 = int.Parse(from_args[3]),
                Range_To = to,
                Range_To_Value = IPAddress.Parse(to).Address,
                Range_To_Octet_1 = int.Parse(to_args[0]),
                Range_To_Octet_2 = int.Parse(to_args[1]),
                Range_To_Octet_3 = int.Parse(to_args[2]),
                Range_To_Octet_4 = int.Parse(to_args[3]),
                Country = country
            }) != null;

            return result;
        }

        public bool DeleteRange(string from, string to, string country)
        {
            return Database.GetCollection<Range>(GetType().Name).Delete(i => i.Range_From.Equals(from) && i.Range_To.Equals(to) && i.Country.Equals(country)) > 0;
        }
    }
}