using ASI.Services.Algolia;
using ASI.Services.Algolia.Client;
using ASI.Services.Algolia.Indexing;
using Microsoft.Extensions.DependencyInjection;
using CompanyProfile.Algolia.Indexes;
using CompanyProfile.Algolia.Querying;
using CompanyProfile.Algolia.Seeding;
using CompanyProfile.Core.Models;
using CompanyProfile.Core.Search.Models;
using CompanyProfile.Core.Search.Providers;

namespace CompanyProfile.Algolia.Startup
{
    public static class DependencyInjection
    {
        public static void AddAlgolia(this IServiceCollection services)
        {
            services.AddAsiAlgolia();
            services.AddAsiAlgoliaIndexing();

            services.AddSingleton<ISearchIndexNameProvider, AlgoliaIndexNameProvider>();

            RegisterSearchProviders(services);
            AddIndexOperationStrategies(services);
            RegisterIndexCreation(services);
            RegisterSeeding(services);
        }

        private static void RegisterSearchProviders(IServiceCollection services)
        {
            services.AddScoped<IMyEntitySearchProvider, MyEntitySearchProvider>();
        }

        private static void AddIndexOperationStrategies(IServiceCollection services)
        {
            services.AddSingleton<IIndexOperationStrategy, DefaultAutomapperIndexOperationStrategy<MyEntity, SearchMyEntity>>();
        }

        private static void RegisterIndexCreation(IServiceCollection services)
        {
            services.AddSingleton<IIndexDefinition<SearchMyEntity>, MyEntityIndexDefinition>();
            services.AddSingleton<IIndexCreationStrategy, DefaultIndexCreationStrategy<SearchMyEntity>>();
            services.AddSingleton<IIndexOperationStrategy, DefaultAutomapperIndexOperationStrategy<MyEntity, SearchMyEntity>>();
        }

        private static void RegisterSeeding(IServiceCollection services)
        {
            services.AddScoped<AlgoliaSeedService>();
            services.AddScoped<ISeeder, MyEntitySeeder>();

            services.AddScoped<SeederProvider>();
        }
    }
}
