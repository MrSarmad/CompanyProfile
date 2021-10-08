using ASI.Console;
using ASI.Console.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using CompanyProfile.Core.Context;
using CompanyProfile.Core.Security;
using CompanyProfile.Core.Startup;
using CompanyProfile.Data.Startup;
using CompanyProfile.Algolia.Startup;

namespace CompanyProfile.Console
{
    public class CompanyProfileContext : CommandContext<CompanyProfileEnvironment>
    {
        public CompanyProfileContext(CommandArguments args, CommandList list, IEnvironmentCollection<CompanyProfileEnvironment> envs)
            : base(args, list, envs)
        {

        }

        public IServiceProvider GetServiceProvider(long tenantId, long userId = 0)
        {
            var services = new ServiceCollection();

            var msConfig = new ConfigurationBuilder()
                .AddJsonFile($"appsettings_{Environment.Env}.json")
                .Build()
                ;
            services.AddSingleton(sp => msConfig);
            services.AddSingleton<IConfiguration>(sp => msConfig);

            services.AddSingleton<IUserInformation>(sp => new ConsoleUserInformation(tenantId, userId));
            services.AddSingleton<ITenantContext>(sp => new ConsoleTenantContext(tenantId));

            services.AddCore();
            services.AddData();
            services.AddAlgolia();

            return services.BuildServiceProvider();
        }

        public class ConsoleUserInformation : IUserInformation
        {
            public string UserName => "Console";
            public string? ImageUrl => null;
            public long UserId { get; }
            public long TenantId { get; set; }
            public long OwnerId { get; set; }
            public IReadOnlyList<long> Teams { get; } = new List<long>();


            //todo: load out teams for given user
            public ConsoleUserInformation(long tenantId, long userId)
            {
                TenantId = tenantId;
                UserId = userId;
            }

        }
    }
}
