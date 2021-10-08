using System;

namespace ASI.Contracts.CompanyProfile
{
    public sealed class MyEntityView
    {
        public long Id { get; set; }
        //This will have a null value for now because we cannot set it at this time, but by denoting it as non-nullable,
        // it signifies that the fulfilment of this contract will ensure this value is set
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public AddressView? PrimaryAddress { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public sealed class AddressView
    {
        public long Id { get; set; }
        public bool IsPrimary { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
    }
}
