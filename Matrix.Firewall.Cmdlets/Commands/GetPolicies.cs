using Matrix.Firewall.Database.Repositories;
using Matrix.Firewall.Database.Repositories.Interfaces;
using System;
using System.Management.Automation;

namespace Matrix.Firewall.Cmdlets.Commands
{
    [OutputType(typeof(Policy))]
    [Cmdlet(VerbsCommon.Get, "Policies")]
    public class GetPolicies : Cmdlet
    {
        private IPolicyRepository Database { get; set; }

        protected override void BeginProcessing()
        {
            Database = new PolicyRepository(Environment.GetEnvironmentVariable("MATRIX.FIREWALL.DATABASE"));
        }

        protected override void ProcessRecord()
        {
            WriteObject(Database.GetPolicies(), true);
        }
    }
}