using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using ASI.Console;
using ASI.Console.Commands;
using log4net;
using log4net.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CompanyProfile.Core.Startup;

namespace CompanyProfile.Console
{
    class Program
    {
        static Program()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        static void Main(string[] args)
        {
            //logging in .net core is odd, seems to require this host building
            //https://www.thecodebuzz.com/logging-using-log4net-net-core-console-application/

            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    //add all the ConsoleCommands to the registry
                    services.RegisterAllTypes<IAsiConsoleCommandBase>(new[]
                    {
                        Assembly.GetExecutingAssembly()!,
                        Assembly.GetAssembly(typeof(AsiConsole))!
                    });

                    services.AddSingleton<IEnvironmentFactory<CompanyProfileEnvironment>, CompanyProfileEnvironmentFactory>();
                    services.AddSingleton<AsiConsole<CompanyProfileEnvironment>>();
                })
                .ConfigureLogging(logBuilder =>
                {
                    logBuilder.AddLog4Net();
                })
                .UseConsoleLifetime();

            var host = builder.Build();
            using (var scope = host.Services.CreateScope())
            {
                var console = scope.ServiceProvider.GetService<AsiConsole<CompanyProfileEnvironment>>()!;
                console.Start(args);
            }
        }
    }
}
