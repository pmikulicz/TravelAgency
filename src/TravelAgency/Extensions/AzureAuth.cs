using System.Collections.Generic;

namespace TravelAgency.Extensions
{
    public class AzureAuth
    {
        public IEnumerable<Role> Roles { get; set; }
    }

    public class Role
    {
        public string Name { get; set; }

        public Claim Claim { get; set; }
    }

    public class Claim
    {
        public string Type { get; set; }

        public IEnumerable<string> Values { get; set; }
    }
}