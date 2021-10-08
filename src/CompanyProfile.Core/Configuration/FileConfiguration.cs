using Microsoft.Extensions.Configuration;
using System;

namespace CompanyProfile.Core.Configuration
{
    public class FileConfiguration : IConfiguration
    {
        public Microsoft.Extensions.Configuration.IConfiguration _config;

        public string? EnvironmentName => _config[nameof(EnvironmentName)];
        public string ApplicationCreationSecret => _config[nameof(ApplicationCreationSecret)];
        public string ConnectionString => _config.GetConnectionString("CompanyProfileConnection");
        public string EsbConnectionString => _config["EsbConnectionString"];

        public FileConfiguration(Microsoft.Extensions.Configuration.IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }        
    }
}
