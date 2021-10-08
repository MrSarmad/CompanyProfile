using Microsoft.Extensions.Configuration;

namespace ASI.Services.CompanyProfile
{
    public interface ICompanyProfileConfiguration
    {
        string BaseUrl { get; set; }
    }

    public class CompanyProfileConfiguration : ICompanyProfileConfiguration
    {
        public CompanyProfileConfiguration(IConfiguration configuration)
        {
            configuration.GetSection("CompanyProfile").Bind(this);
        }

        public string BaseUrl { get; set; }
    }
}
