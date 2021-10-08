using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CompanyProfile.Core.Models;

namespace CompanyProfile.Data.Mappings
{
    public abstract class TenantEntityMapping<T> : EntityMapping<T>
        where T : TenantEntity
    {
        public override void Configure(EntityTypeBuilder<T> b)
        {
            base.Configure(b);

            ConfigureInternal(b);
        }
    }
}
