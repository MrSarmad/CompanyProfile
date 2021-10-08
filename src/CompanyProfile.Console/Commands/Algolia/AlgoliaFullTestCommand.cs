using ASI.Console;
using ASI.Console.Commands;
using ASI.Services.Search.Models;
using Microsoft.Extensions.DependencyInjection;
using CompanyProfile.Core.MyEntities;
using CompanyProfile.Core.Search.Models;
using CompanyProfile.Core.Search.Providers;
using System;
using System.Linq;

namespace CompanyProfile.Console.Commands.Algolia
{
    [Command("algo", "Tests algolia")]
    public class AlgoliaFullTestCommand : ICompanyProfileCommand
    {
        public void Execute(CompanyProfileContext context)
        {
            var sp = context.GetServiceProvider(4396);
            var svc = sp.GetService<IMyEntityService>()!;
            var searchProvider = sp.GetService<IMyEntitySearchProvider>()!;

            var now = DateTime.Now;
            var added = svc.AddAsync(new Core.Models.MyEntity
            {
                Name = $"Algolia Test Command",
                Description = now.ToString()
            }).GetAwaiter().GetResult();
            Terminal.Green($"Added entity {added.Id} - {added.Name} - {added.Description}");

            var res = searchProvider.SearchAsync(new SearchRequest {
                From = 0,
                Term = context.Args.GetOrDefault(0, ""),
            }).GetAwaiter().GetResult();

            foreach (var r in res.Results)
            {
                Terminal.Cyan($"Found {r.Id} - {r.Name} - {r.Description}");
            }
            if (!res.Results.Any())
                Terminal.Red("No Results");
        }
    }
}
