using ASI.Services.Algolia;
using ASI.Services.Algolia.Client;
using ASI.Services.Algolia.Indexing;
using CompanyProfile.Core.Search.Models;
using System.Collections.Generic;

namespace CompanyProfile.Algolia.Indexes
{
    public class MyEntityIndexDefinition : DefaultIndexDefinition<SearchMyEntity>
    {
        protected override void ConfigureIndex_Internal(IIndexSettingsDescriptor<SearchMyEntity> builder)
        {
            builder.Ranking(x => x.Descending(x => x.UpdateDate!))
                .AttributesForFaceting(x => x
                    .Searchable(y => y.TenantId)
                    .NotSearchable(y => y.Id)
                    )
                .SearchableAttributes(s => s
                    .Ordered(o => o.Name)
                    .Ordered(o => o.Description!))
                .Replicas(r => r
                    .AddReplica("az", builder, rs =>
                        rs.Ranking(y => y.None().Ascending(x => x.Name))
                    )
                    .AddReplica("za", builder, rs =>
                        rs.Ranking(y => y.None().Descending(x => x.Name))
                    ))
                ;
        }
    }
}
