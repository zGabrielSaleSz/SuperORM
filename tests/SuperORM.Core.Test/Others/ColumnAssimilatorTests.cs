using SuperORM.Core.Domain.Service.LinqSQL.SelectableTools;
using SuperORM.TestsResource.Entities;
using Xunit;

namespace SuperORM.Core.Test.Others
{
    public class ColumnAssimilatorTests
    {
        [Fact]
        public void Should_ReturnRespectiveColumnByEntityProperty_When_UsingColumnAssimilator()
        {
            // Arrange
            ColumnAssimilator columnAssimilator = new ColumnAssimilator();
            columnAssimilator.Add<User>("id", "ID");
            columnAssimilator.Add<User>("Name", "Name");
            columnAssimilator.Add<User>("email", "Email");
            columnAssimilator.Add<User>("password", "Password");

            columnAssimilator.Add<OldUser>("ID", "id");
            columnAssimilator.Add<OldUser>("Name", "strName");
            columnAssimilator.Add<OldUser>("Active", "blnActive");

            columnAssimilator.Add<Document>("id", "documentId");

            // Act + Assert
            Assert.Equal("ID", columnAssimilator.GetByProperty<User>("id"));
            Assert.Equal("Name", columnAssimilator.GetByProperty<User>("Name"));
            Assert.Equal("Email", columnAssimilator.GetByProperty<User>("email"));
            Assert.Equal("Password", columnAssimilator.GetByProperty<User>("password"));

            Assert.Equal("id", columnAssimilator.GetByProperty<OldUser>("ID"));
            Assert.Equal("strName", columnAssimilator.GetByProperty<OldUser>("Name"));
            Assert.Equal("blnActive", columnAssimilator.GetByProperty<OldUser>("Active"));

            Assert.Equal("documentId", columnAssimilator.GetByProperty<Document>("id"));
        }

        [Fact]
        public void Should_ReturnRespectivePropertyByColumnName_When_UsingColumnAssimilator()
        {
            // Arrange
            ColumnAssimilator columnAssimilator = new ColumnAssimilator();
            columnAssimilator.Add<User>("id", "ID");
            columnAssimilator.Add<User>("Name", "Name");
            columnAssimilator.Add<User>("email", "Email");
            columnAssimilator.Add<User>("password", "Password");

            columnAssimilator.Add<OldUser>("ID", "id");
            columnAssimilator.Add<OldUser>("Name", "strName");
            columnAssimilator.Add<OldUser>("Active", "blnActive");

            columnAssimilator.Add<Document>("id", "documentId");

            // Act + Assert
            Assert.Equal("id", columnAssimilator.GetByColumnValue<User>("ID"));
            Assert.Equal("Name", columnAssimilator.GetByColumnValue<User>("Name"));
            Assert.Equal("email", columnAssimilator.GetByColumnValue<User>("Email"));
            Assert.Equal("password", columnAssimilator.GetByColumnValue<User>("Password"));

            Assert.Equal("ID", columnAssimilator.GetByColumnValue<OldUser>("id"));
            Assert.Equal("Name", columnAssimilator.GetByColumnValue<OldUser>("strName"));
            Assert.Equal("Active", columnAssimilator.GetByColumnValue<OldUser>("blnActive"));

            Assert.Equal("id", columnAssimilator.GetByColumnValue<Document>("documentId"));
        }
    }
}
