using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;
using ASI.Services.Http.Security;
namespace ASI.Services.CompanyProfile
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Add ability to connect to CompanyProfile via injecting an <see cref="ICompanyProfileClient"/>.
        /// </summary>
        /// <param name="services"></param>
        public static void AddCompanyProfile(this IServiceCollection services)
        {
            services.AddSingleton<ICompanyProfileConfiguration, CompanyProfileConfiguration>();
            services.AddScoped<ICompanyProfileClient, CompanyProfileClient>();
            //services.AddSingleton<IGetString, GetString1>();
            //services.AddRestClient()
            //services.AddRestClient((o) => o.BaseUrl = "http://www.test.com".ToAbsoluteUrl());

            services.AddHttpClient(CompanyProfileClient.HttpClientName, (sp, client) =>
            {
                var config = sp.GetRequiredService<ICompanyProfileConfiguration>();
                if (config.BaseUrl == null)
                    throw new InvalidOperationException("CompanyProfile:BaseUrl must be configured");
                client.BaseAddress = new Uri(config.BaseUrl);

                var provider = sp.GetRequiredService<IAsiTokenProvider>();
                var token = provider.GetToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            });

        }
    }
}
