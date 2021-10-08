using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using CompanyProfile.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using CompanyProfile.Core.Context;
using ASI.Services.Search.Indexing;
using Moq;

namespace CompanyProfile.IntegrationTest
{
    public class CompanyProfileWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private class TestSearchTransactionProvider : ISearchTransactionProvider
        {
            public ISearchTransaction CreateTransaction()
            {
                return new Mock<ISearchTransaction>().Object;
            }
        }

        private void RemoveService<T>(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(T));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the app's ApplicationDbContext registration.
                RemoveService<DbContextOptions<CompanyProfileDbContext>>(services);
                RemoveService<ISearchTransactionProvider>(services);
                services.AddSingleton<ISearchTransactionProvider, TestSearchTransactionProvider>();
                //RemoveService<IUserInformation>(services);
                //todo: configure custom userinfo here 

                // Add ApplicationDbContext using an in-memory database for testing.
                services.AddDbContext<CompanyProfileDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                // context (ApplicationDbContext).
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetService<CompanyProfileDbContext>()!;
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CompanyProfileWebApplicationFactory<TStartup>>>();

                    // Ensure the database is created.
                    db.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with test data.
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }
            });
        }


    }
}
