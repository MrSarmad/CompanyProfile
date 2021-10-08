using System.Collections.Generic;

namespace CompanyProfile.Core.Models
{
    public sealed class MyEntity : TenantEntity
    {
        //use a string? when this will not be a required field. 
        //null! stops a compiler warning. Since this is a DB required field we can suppress the warning here.
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public long OwnerId { get; set; }

        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }

    public sealed class Address : Entity
    {
        public bool IsPrimary { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
    }
}
