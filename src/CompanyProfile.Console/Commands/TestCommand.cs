using ASI.Console.Commands;
using CompanyProfile.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using CompanyProfile.Core.Models;
using System.Linq;
using ASI.Console;
using Microsoft.Extensions.Logging;

namespace CompanyProfile.Console.Commands
{
    [Command("test")]
    public class TestCommmand : ICompanyProfileCommand
    {
        private readonly ILogger<TestCommmand> _logger;

        public TestCommmand(ILogger<TestCommmand> logger)
        {
            _logger = logger;
        }

        public void Execute(CompanyProfileContext context)
        {
            _logger.LogInformation("Test Command Ran");
            Terminal.Green("Test command ran");
        }
    }

    [Command("db-test")]
    public class DbTestCommmand : ICompanyProfileCommand
    {
        public void Execute(CompanyProfileContext context)
        {
            var sp = context.GetServiceProvider(4396);

            var da = sp.GetService<IDataAccess>()!;

            var id = context.Args.GetOrDefault(0, 123L);
            var myEntity = da.QueryTenant<MyEntity>()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (myEntity != null)
                Terminal.Green($"Found entity {id}: {myEntity.Name}");
            else
                Terminal.Red($"Couldn't find entity {id}");
        }
    }
}