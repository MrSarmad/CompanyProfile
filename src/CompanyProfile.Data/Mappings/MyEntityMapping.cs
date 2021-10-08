using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CompanyProfile.Core.Models;

namespace CompanyProfile.Data.Mappings
{
    public sealed class MyEntityMapping : TenantEntityMapping<MyEntity>
    {
        protected override void ConfigureInternal(EntityTypeBuilder<MyEntity> b)
        {
        }
    }
}
