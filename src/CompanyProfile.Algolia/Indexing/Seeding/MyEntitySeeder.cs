using ASI.Services.Algolia;
using ASI.Services.Algolia.Client;
using ASI.Services.Search.Indexing;
using Microsoft.Extensions.Logging;
using CompanyProfile.Core.Data;
using CompanyProfile.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace CompanyProfile.Algolia.Seeding
{
    /// <summary>
    /// Example seeder implementation
    /// </summary>
    public sealed class MyEntitySeeder : SeederBase<MyEntity>
    {
        public override IndexType Type => IndexType.MyEntity;

        public MyEntitySeeder(IDataAccess dataAccess, ISearchIndexProvider algoliaIndexProvider,
            ISearchTransactionProvider searchTransactionProvider,
            ISearchIndexNameProvider searchIndexNameProvider, ILoggerFactory loggerFactory)
            : base(dataAccess, algoliaIndexProvider, searchTransactionProvider, searchIndexNameProvider, loggerFactory)
        {
        }

        public override IQueryable<MyEntity> BaseQuery(AlgoliaSeedOptions options)
        {
            return _dataAccess.Query<MyEntity>()
                .Where(x => x.IsDeleted == false
                    && (options.TenantId == 0 || x.TenantId == options.TenantId));
        }

        public override IQueryable<MyEntity> LoadQuery(int page, AlgoliaSeedOptions options)
        {
            return _dataAccess.Query<MyEntity>(/* Include joins here*/)
                .Where(x => x.IsDeleted == false
                    && (options.TenantId == 0 || x.TenantId == options.TenantId))
                .OrderByDescending(x => x.Id);
        }

        public override void PostLoadStep(List<MyEntity> records)
        {
            foreach (var record in records)
            {
                //go to an external service here to populate additional data if needed
                //_collaboratorLoader.PopulateCollaboratorAsync(record).GetAwaiter().GetResult();
            }
        }
    }
}
