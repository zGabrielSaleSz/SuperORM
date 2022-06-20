using SuperORM.Core.Domain.Model.Enum;
using SuperORM.Core.Domain.Model.QueryBuilder;
using SuperORM.Core.Domain.Model.QueryBuilder.Fields;
using SuperORM.Core.Domain.Model.QueryBuilder.Fields.FieldsArgument;
using SuperORM.Core.Domain.Service.QueryBuilder;
using SuperORM.MySql;
using Xunit;

namespace SuperORM.Core.Test.QueryBuilder.Deletable
{
    public class DeletableBuilderMySqlTests
    {
        [Fact]
        public void Should_BuildQuery_When_Insertable()
        {
            // Arrange
            string expected = "DELETE FROM `users` WHERE (`users`.`id` >= 10)";
            DeletableBuilder deletableBuilder = new DeletableBuilder(new QuerySintax());

            Table table = new Table("users");
            IField fieldId = table.AddField<Column>("id");

            // Act
            string actual = deletableBuilder
                .From("users")
                .Where(fieldId, SqlComparator.GreaterThanOrEqual, 10)
                .GetQuery();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
