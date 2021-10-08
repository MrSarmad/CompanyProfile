using ASI.Console.Commands;
using Microsoft.Extensions.DependencyInjection;
using CompanyProfile.Algolia.Seeding;

namespace CompanyProfile.Console.Commands.Algolia
{
    [Command("algo-seed", "Seeds database")]
    public class AlgoliaSeedCommand : ICompanyProfileCommand
    {
        public void Execute(CompanyProfileContext context)
        {
            var tenantId = context.Args.GetOrDefault(0, 4396);
            var sp = context.GetServiceProvider(tenantId);

            //

            var svc = sp.GetService<AlgoliaSeedService>()!;
            var options = new AlgoliaSeedOptions
            {
                Types = IndexType.All,
                TenantId = 0
            };
            svc.RunSeed(options);
        }
    }
}
