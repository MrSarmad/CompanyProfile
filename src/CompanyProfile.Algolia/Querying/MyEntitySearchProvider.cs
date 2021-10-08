using Algolia.Search.Models.Search;
using ASI.Services.Access;
using ASI.Services.Algolia;
using ASI.Services.Algolia.Client;
using ASI.Services.Algolia.Search;
using ASI.Services.Search.Models;
using Microsoft.Extensions.Logging;
using CompanyProfile.Core.Models;
using CompanyProfile.Core.Search.Models;
using CompanyProfile.Core.Search.Providers;
using CompanyProfile.Core.Security;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProfile.Algolia.Querying
{
    public sealed class MyEntitySearchProvider : SearchProviderBase<SearchMyEntity>, IMyEntitySearchProvider
    {
        private readonly ISecurityPolicy _securityPolicy;
        private readonly ICurrentUser _currentUser;

        public MyEntitySearchProvider(ISecurityPolicy securityPolicy, ISearchIndexProvider indexProvider, ICurrentUser currentUser,
            ILoggerFactory loggerFactory)
            : base(indexProvider, loggerFactory)
        {
            _securityPolicy = securityPolicy;
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        public override SearchResult<SearchMyEntity> TransformResult(SearchRequest request, SearchResponse<SearchMyEntity> searchResponse)
        {
            var result = new SearchResult<SearchMyEntity>
            {
                ResultsTotal = searchResponse.NbHits,
                Results = searchResponse.Hits
                    .ToList(),
            };

            return result;
        }

        protected override Query BuildSearchQuery(SearchRequest criteria)
        {
            var builder = new MyEntitySearchQueryBuilder()
                .WithType(criteria.Type, _currentUser.OwnerId)
                ;

            return builder.Build(criteria);
        }
    }
}
