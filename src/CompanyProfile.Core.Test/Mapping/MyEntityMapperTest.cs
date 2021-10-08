using ASI.Contracts.CompanyProfile;
using FluentAssertions;
using CompanyProfile.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CompanyProfile.Core.Test.Mapping
{

    public class MyEntityMapperTest : MapperTest
    {
        [Fact]
        public void ToViewModel_Test()
        {
            var source = new MyEntity
            {
                Name = "MyName",
                Description = "Desc",
                Addresses = new List<Address>
                {
                    new Address
                    {
                        Id = 1,
                        IsPrimary = false,
                        City = "not",
                        Street = "this one"
                    },
                    new Address
                    {
                        Id = 2,
                        IsPrimary = true,
                        City = "this",
                        Street = "one"
                    }
                },
                CreateDate = new DateTime(2000, 1, 1)
            };

            var result = Mapper.Map<MyEntityView>(source);

            result.Name.Should().Be(source.Name);
            result.Description.Should().Be(source.Description);
            result.PrimaryAddress.Should().NotBeNull();
            result.PrimaryAddress!.Id.Should().Be(2);
            result.PrimaryAddress.City.Should().Be("this");
            result.PrimaryAddress.Street.Should().Be("one");
            result.CreateDate.Should().Be(new DateTime(2000, 1, 1));
        }

        [Fact]
        public void ToViewModel_NoAddress_Test()
        {
            var source = new MyEntity
            {
                Name = "MyName",
                Description = "Desc",
            };

            var result = Mapper.Map<MyEntityView>(source);

            result.Name.Should().Be(source.Name);
            result.Description.Should().Be(source.Description);
            result.PrimaryAddress.Should().BeNull();
        }
    }
}
