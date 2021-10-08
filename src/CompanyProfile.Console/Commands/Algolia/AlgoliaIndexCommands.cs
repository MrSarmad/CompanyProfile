using ASI.Console;
using ASI.Console.Commands;
using ASI.Services.Algolia;
using ASI.Services.Algolia.Client;
using ASI.Services.Algolia.Indexing;
using Microsoft.Extensions.DependencyInjection;
using CompanyProfile.Core.Models;

namespace CompanyProfile.Console.Commands.Algolia
{
    [Command("algo-index-create", "Add product to a collection")]
    public class AlgoliaIndexCreateCommand : ICompanyProfileCommand
    {
        public void Execute(CompanyProfileContext context)
        {
            var sp = context.GetServiceProvider(4396);

            var creator = sp.GetService<AlgoliaIndexCreator>()!;
            creator.CreateAllIndexes();
        }
    }

    [Command("algo-index-settings-get", "Get settings for an index")]
    public class AlgoliaIndexSettingsCommand : ICompanyProfileCommand
    {
        public void Execute(CompanyProfileContext context)
        {
            var sp = context.GetServiceProvider(4396);

            var clientProvider = sp.GetService<ISearchClientProvider>()!;
            var client = clientProvider.GetClient();
            var index = client.InitIndex<MyEntity>();

            var settings = index.GetSettings();
            Terminal.Json.Blue(settings);
        }
    }
}
