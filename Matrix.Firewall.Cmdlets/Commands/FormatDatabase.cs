using LiteDB;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Net;

namespace Matrix.Firewall.Cmdlets.Commands
{
    [OutputType(typeof(FormatDatabaseResult))]
    [Cmdlet(VerbsCommon.Format, "Database")]
    public class FormatDatabase : Cmdlet
    {
        [Parameter]
        public string Source { get; set; } // https://db-ip.com/db/download/country

        [Parameter]
        public string Destination { get; set; }

        private LiteDatabase Database { get; set; }

        protected override void BeginProcessing()
        {
            Database = new LiteDatabase(Destination);
        }

        protected override void ProcessRecord()
        {
            var index = 0;

            var countries = new Dictionary<string, Country>();

            countries.Add("XX", new Country() { Code = "XX", Name = "Unknown" });
            countries.Add("ZZ", new Country() { Code = "ZZ", Name = "Localhost" });

            using (var reader = new StreamReader(Source))
            {
                var line = reader.ReadLine();

                while (!string.IsNullOrEmpty(line))
                {
                    if (!line.Contains(":"))
                    {
                        var args = line.Split(',');

                        var from = args[0].Trim('"').Split('.');
                        var to = args[1].Trim('"').Split('.');

                        Database.GetCollection<Range>("RangeRepository").Insert(new Range()
                        {
                            Range_From = args[0].Trim('"'),
                            Range_From_Value = IPAddress.Parse(args[0].Trim('"')).Address,
                            Range_From_Octet_1 = int.Parse(from[0]),
                            Range_From_Octet_2 = int.Parse(from[1]),
                            Range_From_Octet_3 = int.Parse(from[2]),
                            Range_From_Octet_4 = int.Parse(from[3]),
                            Range_To = args[1].Trim('"'),
                            Range_To_Value = IPAddress.Parse(args[1].Trim('"')).Address,
                            Range_To_Octet_1 = int.Parse(to[0]),
                            Range_To_Octet_2 = int.Parse(to[1]),
                            Range_To_Octet_3 = int.Parse(to[2]),
                            Range_To_Octet_4 = int.Parse(to[3]),
                            Country = args[2].Trim('"')
                        });

                        var countryCode = args[2].Trim('"');

                        if (!countries.ContainsKey(countryCode))
                            countries.Add(countryCode, GetCountry(countryCode));

                        index++;

                        WriteProgress(new ProgressRecord(1, "Formatting database...", $"Writing record {index}"));
                    }

                    line = reader.ReadLine();
                }
            }

            foreach (var i in countries)
                Database.GetCollection<Country>("CountryRepository").Insert(i.Value);

            WriteObject(new FormatDatabaseResult() { Message = "Database format completed." });
        }

        private Country GetCountry(string countryCode)
        {
            var result = new Country() { Name = "Unknown", Code = countryCode };

            WriteProgress(new ProgressRecord(2, "Country Information", $"Country code {countryCode} = ???"));

            var response = new WebClient().DownloadString("https://restcountries.eu/rest/v2/alpha/" + countryCode);

            if (!string.IsNullOrEmpty(response))
                result.Name = JsonConvert.DeserializeObject<dynamic>(response)?.name;

            WriteProgress(new ProgressRecord(2, "Country Information", $"Country Code: {countryCode} = {result.Name}"));

            return result;
        }
    }

    public class FormatDatabaseResult
    {
        public string Message { get; set; }
    }
}