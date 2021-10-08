using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using CompanyProfile.Core.Configuration;

namespace CompanyProfile.Data.Context
{
    public sealed class CompanyProfileDbContextFactory : IDesignTimeDbContextFactory<CompanyProfileDbContext>
    {
        public IConfigurationRoot _config;
        public string _clientCertPath = "";
        public string _connString = "";

        public CompanyProfileDbContextFactory()
        {
            var env = "UAT";

            _config = new ConfigurationBuilder()
                .AddJsonFile("dbsettings.json")
                .Build();
            _connString = _config.GetConnectionString("ZealConnection_" + env);

            _clientCertPath = "";
        }

        /// <summary>
        /// this is the endpoint used in Package Manager console. Change this to update a different database
        /// </summary>
        public CompanyProfileDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CompanyProfileDbContext>();
            //optionsBuilder.UseSqlServer(_connString); //if using sql
            optionsBuilder.UseNpgsql(_connString, x =>
            {
            });
            return new CompanyProfileDbContext(optionsBuilder.Options, new MockConfiguration { EnvironmentName = "DEV" }, null!);
        }
    }
}
