using FluentAssertions;
using Moq;
using CompanyProfile.Core.Context;
using CompanyProfile.Core.Data;
using CompanyProfile.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CompanyProfile.Core.Test.Data
{
    public class AuditFieldFixerTest
    {
        public class TempEntity : IEntity
        {
            public long Id { get; set; }
            public bool IsDeleted { get; set; }
            public DateTime CreateDate { get; set; }
            public string CreatedBy { get; set; } = null!;
            public DateTime? UpdateDate { get; set; }
            public string? UpdatedBy { get; set; }
        }

        [Fact]
        public void AuditFix_Create_Test()
        {
            var user = new Mock<IUserInformation>();
            user.Setup(x => x.UserName).Returns("test");
            var target = new AuditFieldFixer(user.Object);

            var obj = new TempEntity();
            target.FixAuditFields(obj, DbEntityState.Added);

            obj.CreateDate.Should().NotBe(DateTime.MinValue);
            obj.CreatedBy.Should().Be("test");
            obj.UpdateDate.Should().BeNull();
            obj.UpdatedBy.Should().BeNull();
        }

        [Fact]
        public void AuditFix_Update_Test()
        {
            var user = new Mock<IUserInformation>();
            user.Setup(x => x.UserName).Returns("test");
            var target = new AuditFieldFixer(user.Object);

            var obj = new TempEntity();
            obj.CreateDate = new DateTime(2000, 1, 1);
            obj.CreatedBy = "asdf";
            target.FixAuditFields(obj, DbEntityState.Modified);

            obj.CreateDate.Should().Be(new DateTime(2000, 1, 1));
            obj.CreatedBy.Should().Be("asdf");
            obj.UpdateDate.Should().NotBeNull();
            obj.UpdatedBy.Should().NotBeNull();
        }
    }
}
