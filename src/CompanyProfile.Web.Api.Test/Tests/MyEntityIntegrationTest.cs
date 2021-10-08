using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CompanyProfile.IntegrationTest.Tests;
using FluentAssertions;
using Xunit;
using CompanyProfile.Web;
using System.Threading.Tasks;
using CompanyProfile.Web.Api;
using ASI.Contracts.CompanyProfile;

namespace CompanyProfile.IntegrationTest.Tests
{
    public class MyEntityIntegrationTest : IntegrationTestBase
    {

        public MyEntityIntegrationTest(CompanyProfileWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task AddModel()
        {
            var model = new MyEntityView
            {
                Name = nameof(AddModel),
                Description = "View Description",
            };

            var retValue = await PostAsync<MyEntityView>("api/myentities", model);
            retValue.Id.Should().NotBe(0);
            retValue.Name.Should().Be(nameof(AddModel));
            retValue.Description.Should().Be("View Description");
            retValue.CreateDate.Should().NotBe(DateTime.MinValue);
        }

        [Fact]
        public async Task AddModel_UpdateModel()
        {
            var model = new MyEntityView
            {
                Name = nameof(AddModel_UpdateModel),
                Description = "View Description",
            };

            var addValue = await PostAsync<MyEntityView>("api/myentities", model);
            addValue.Id.Should().NotBe(0);
            addValue.Name.Should().Be(nameof(AddModel_UpdateModel));
            addValue.Description.Should().Be("View Description");

            //change desc
            addValue.Description = "New Description";

            var updateRet = await PutAsync<MyEntityView>($"api/myentities/{addValue.Id}", addValue);
            updateRet.Id.Should().Be(addValue.Id);//id shouldn't not change
            updateRet.Name.Should().Be(nameof(AddModel_UpdateModel)); //name should not change
            updateRet.Description.Should().Be("New Description"); //description should change
        }
    }
}