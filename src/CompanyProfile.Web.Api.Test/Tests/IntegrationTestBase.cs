using ASI.Services.Http.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using CompanyProfile.Core.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CompanyProfile.IntegrationTest.Tests
{
    [Trait("IntegrationTest", "true")]
    public class IntegrationTestBase : IClassFixture<CompanyProfileWebApplicationFactory<Web.Api.Startup>>
    {
        private readonly CompanyProfileWebApplicationFactory<Web.Api.Startup> _factory;

        protected IntegrationTestBase(CompanyProfileWebApplicationFactory<Web.Api.Startup> factory)
        {
            _factory = factory;
            _config = new FileConfiguration();
        }

        private readonly FileConfiguration _config;

        protected virtual bool UseMockAuthentication => true;
        protected virtual bool UseAuthentication => !_config.SUTIsLocal;

        private HttpClient? _client;

        protected HttpClient GetHttpClient()
        {
            if (_client != null)
                return _client;
            //only use mock auth in local
            if (UseMockAuthentication && _config.SUTIsLocal)
                return _client = GetMockAuthenticatedHttpClient();
            else if (UseAuthentication)
                return _client = GetAuthenticatedHttpClient();
            return _client = GetUnauthenticatedHttpClient();
        }


        protected HttpClient GetMockAuthenticatedHttpClient()
        {
            if (_config.SUTIsLocal)
            {
                return _factory.WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddAuthentication("Test")
                            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                                "Test", options => { });

                        var authUserProvider = new Mock<IAuthenticatedUserProvider>();
                        var userMock = new Mock<ASI.Services.Security.AuthenticatedUser>();
                        userMock.SetupGet(x => x.Name).Returns("Integration Test User");
                        userMock.Object.UserId = 1234;
                        userMock.Object.ApplicationId = 5678;
                        authUserProvider.Setup(x => x.GetUser()).Returns(userMock.Object);
                        services.AddSingleton<IAuthenticatedUserProvider>(sp => authUserProvider.Object);
                    });
                })
                .CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });
            }

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_config.SystemUnderTest);
            return httpClient;
        }

        protected HttpClient GetAuthenticatedHttpClient()
        {
            var accessToken = ""; // insert code to get access token
            //var accessToken = TestUserAuthenticator.GetTokens().access;
            var httpClient = new HttpClient();

            //if (_config.SUTIsLocal)
            //    httpClient = _factory.CreateClient();
            //else
            {

                var url = _config.SystemUnderTest ?? "http://zeal.local-asicentral.com/home";
                httpClient.BaseAddress = new Uri(url);
            }

            //legacy
            //httpClient.DefaultRequestHeaders.Authorization =
            //    new AuthenticationHeaderValue("AsiMemberAuth", $"guid=\"{accessToken}\"");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return httpClient;
        }

        protected HttpClient GetUnauthenticatedHttpClient()
        {
            var sut = _config.SystemUnderTest;
            if (_config.SUTIsLocal)
                return _factory.CreateClient();

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(sut);
            return httpClient;
        }

        protected async Task<T> GetAsync<T>(string path)
        {
            Console.WriteLine($"GET {path}");
            var client = GetHttpClient();
            var res = await client.GetAsync(path);
            await HttpHelpers.HandleStatusCode(path, res, "GET");
            var str = await res.Content.ReadAsStringAsync();
            Console.WriteLine(str);
            return JsonConvert.DeserializeObject<T>(str);
        }

        protected async Task<T> PostAsync<T>(string path)
        {
            Console.WriteLine($"POST {path}");
            var client = GetHttpClient();
            var res = await client.PostAsync(path, null);
            await HttpHelpers.HandleStatusCode(path, res, "POST");
            var str = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(str);
        }

        protected async Task<T> PostAsync<T>(string path, object o)
        {
            Console.WriteLine($"POST {path}");
            var client = GetHttpClient();
            string json = JsonConvert.SerializeObject(o);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await client.PostAsync(path, content);
            await HttpHelpers.HandleStatusCode(path, res, "POST");
            var str = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(str);
        }

        protected async Task<T> PutAsync<T>(string path)
        {
            Console.WriteLine($"PUT {path}");
            var client = GetHttpClient();
            var res = await client.PutAsync(path, null);
            await HttpHelpers.HandleStatusCode(path, res, "PUT");
            var str = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(str);
        }

        protected async Task<T> PutAsync<T>(string path, object o)
        {
            Console.WriteLine($"PUT {path}");
            var client = GetHttpClient();
            string json = JsonConvert.SerializeObject(o);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await client.PutAsync(path, content);
            await HttpHelpers.HandleStatusCode(path, res, "PUT");
            var str = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(str);
        }

        protected async Task PutAsync(string path, object o)
        {
            Console.WriteLine($"PUT {path}");
            var client = GetHttpClient();
            string json = JsonConvert.SerializeObject(o);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await client.PutAsync(path, content);
            await HttpHelpers.HandleStatusCode(path, res, "PUT");
        }

        protected async Task<string> DeleteAsync(string path)
        {
            Console.WriteLine($"DELETE {path}");
            var client = GetHttpClient();
            var res = await client.DeleteAsync(path);
            await HttpHelpers.HandleStatusCode(path, res, "DELETE");
            var str = await res.Content.ReadAsStringAsync();
            return str;
        }
    }

    public class FileConfiguration : Core.Configuration.IConfiguration
    {
        protected IConfigurationRoot _config;

        public FileConfiguration()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public string SystemUnderTest => _config[nameof(SystemUnderTest)];

        public string? EnvironmentName => _config[nameof(EnvironmentName)];
        public string ConnectionString => _config.GetConnectionString("CompanyProfileDatabase");
        public string EsbConnectionString => _config["EsbConnectionString"];

        public bool SUTIsLocal => string.IsNullOrWhiteSpace(SystemUnderTest) || SystemUnderTest.ToLowerInvariant() == "local";

    }
}
