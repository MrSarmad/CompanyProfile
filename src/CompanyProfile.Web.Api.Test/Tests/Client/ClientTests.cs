using ASI.Services.CompanyProfile;
using Moq;
using CompanyProfile.IntegrationTest;
using CompanyProfile.IntegrationTest.Tests;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CompanyProfile.Web.Api;
using ASI.Contracts.CompanyProfile;
using FluentAssertions;
using Xunit;

namespace CompanyProfile.Web.Api.Test.Tests.Client
{
    /// <summary>
    /// The same tests can be ran using the client app via ASI.Services.CompanyProfile
    /// </summary>
    public class ClientTests : IntegrationTestBase
    {
        public ClientTests(CompanyProfileWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        private CompanyProfileClient GetClient()
        {

            var clientFactory = new Mock<IHttpClientFactory>();
            clientFactory.Setup(x => x.CreateClient(CompanyProfileClient.HttpClientName))
                .Returns(GetHttpClient());
            var client = new CompanyProfileClient(clientFactory.Object);
            return client;
        }

        [Fact]
        public async Task Client_Add_Test()
        {
            var client = GetClient();
            var res = await client.AddMyEntityAsync(new MyEntityView
            {
                Name = "Test From Client",
                Description = "Test from ASI.Services.CompanyProfile"
            });

            res.Id.Should().NotBe(0);
        }

        [Fact]
        public async Task Client_Add_Get_Test()
        {
            var client = GetClient();
            var res = await client.AddMyEntityAsync(new MyEntityView
            {
                Name = "Test From Client",
                Description = "Test from ASI.Services.CompanyProfile"
            });

            res.Id.Should().NotBe(0);

            var getRes = await client.GetMyEntityAsync(res.Id);
            getRes.Name.Should().Be("Test From Client");
        }

        [Fact]
        public async Task Client_Add_NoName_BadRequest_Test()
        {
            var client = GetClient();

            await Assert.ThrowsAsync<CompanyProfileBadRequestException>(async () => await client.AddMyEntityAsync(new MyEntityView
            {
                Name = string.Empty,
                Description = "Should throw bad request, name is required"
            }));
        }
    }
}
