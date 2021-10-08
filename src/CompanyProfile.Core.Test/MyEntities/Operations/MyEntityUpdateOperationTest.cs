using FluentAssertions;
using Moq;
using CompanyProfile.Core.Models;
using CompanyProfile.Core.MyEntities;
using CompanyProfile.Core.MyEntities.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CompanyProfile.Core.Test.MyEntities.Operations
{
    public class MyEntityUpdateOperationTest
    {
        [Fact]
        public void MyEntity_Update_Test()
        {
            //AAA
            //Arrange - Act - Assert

            //arrange
            var id = 1;
            var trans = new TestDataTransaction();
            var searchTrans = new TestSearchTransaction();
            var loader = new Mock<IMyEntityLoader>();

            loader.Setup(x => x.Get(id)).Returns(new MyEntity
            {
                Name = "Original Name"
            });

            var ctx = new MyEntityUpdateContext(id, e =>
            {
                e.Name = "New Name";
            }, loader.Object, trans, searchTrans);

            var op = new MyEntityUpdateOperation(ctx);

            //Act
            op.Load();
            op.StageChanges();

            //Assert
            trans.UpdatedEntityOfType<MyEntity>().Count().Should().Be(1);
            ctx.Entity!.Name.Should().Be("New Name");

            searchTrans.UpdatedOfType<MyEntity>().Count().Should().Be(1);
        }

        [Fact]
        public void MyEntity_Load_Test()
        {
            //AAA
            //Arrange - Act - Assert

            //arrange
            var id = 1;
            var trans = new TestDataTransaction();
            var searchTrans = new TestSearchTransaction();
            var loader = new Mock<IMyEntityLoader>();

            loader.Setup(x => x.Get(id)).Returns(new MyEntity
            {
                Name = "Original Name"
            }).Verifiable();

            var ctx = new MyEntityUpdateContext(id, e =>
            {
                e.Name = "New Name";
            }, loader.Object, trans, searchTrans);

            var op = new MyEntityUpdateOperation(ctx);

            //Act
            op.Load();

            loader.VerifyAll();
        }
    }
}