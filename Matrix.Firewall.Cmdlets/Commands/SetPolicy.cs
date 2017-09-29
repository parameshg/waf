using Matrix.Firewall.Database.Repositories;
using Matrix.Firewall.Database.Repositories.Interfaces;
using System;
using System.Management.Automation;

namespace Matrix.Firewall.Cmdlets.Commands
{
    [OutputType(typeof(SetPolicyResult))]
    [Cmdlet(VerbsCommon.Set, "Policy")]
    public class SetPolicy : Cmdlet
    {
        [Parameter]
        public string Country { get; set; }

        [Parameter]
        public bool Permission { get; set; }

        private IPolicyRepository Database { get; set; }

        protected override void BeginProcessing()
        {
            Database = new PolicyRepository(Environment.GetEnvironmentVariable("MATRIX.FIREWALL.DATABASE"));
        }

        protected override void ProcessRecord()
        {
            Database.SavePolicy(Country, Permission);

            WriteObject(new SetPolicyResult() { Message = $"Policy has been applied for country {Country}." });
        }
    }

    public class SetPolicyResult
    {
        public string Message { get; set; }
    }
}