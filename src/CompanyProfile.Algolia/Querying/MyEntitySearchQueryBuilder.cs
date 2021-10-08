using ASI.Services.Algolia.Search;
using CompanyProfile.Core.Search.Models;

namespace CompanyProfile.Algolia.Querying
{
    public sealed class MyEntitySearchQueryBuilder : SearchQueryBuilderBase<SearchMyEntity, MyEntitySearchQueryBuilder>
    {
        public MyEntitySearchQueryBuilder WithType(string? type, long ownerId)
        {
            if (string.IsNullOrWhiteSpace(type))
                return this;

            if (type.ToLowerInvariant() == "me")
            {
                Descriptor.Filters(x => x.OwnerId == ownerId);
            }
            else if (type.ToLowerInvariant() == "shared")
            {
                Descriptor.Filters(x => x.OwnerId != ownerId);
            }

            return this;
        }
    }
}
