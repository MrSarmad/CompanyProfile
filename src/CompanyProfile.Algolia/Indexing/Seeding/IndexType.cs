using System;

namespace CompanyProfile.Algolia.Seeding
{
    [Flags]
    public enum IndexType
    {
        None = 0,
        All = ~0,
        MyEntity = 0b1,
    }
}
