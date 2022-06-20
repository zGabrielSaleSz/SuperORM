using SuperORM.Core.Domain.Model.QueryBuilder;
using SuperORM.Core.Domain.Model.QueryBuilder.Fields;
using SuperORM.Core.Domain.Model.QueryBuilder.Fields.FieldsArgument;
using SuperORM.Core.Domain.Service.QueryBuilder;
using SuperORM.Core.Interface.QueryBuilder;
using SuperORM.MySql;
using Xunit;

namespace SuperORM.Core.Test.Complement.QueryBuilder.Updatable
{
    public class UpdatableBuilderMySql
    {
        [Fact]
        public void Should_BuildQuery_When_Updatable()
        {
            //Arrange
            string expected = "UPDATE `users` SET `users`.`Nome` = 'Say my name!' WHERE (`users`.`id` = 3)";
            IUpdatableBuilder insertable = new UpdatableBuilder(new QuerySintax());

            Table table = new Table("users");
            IField fieldId = table.AddField<Column>("id");
            IField fieldName = table.AddField<Column>("Nome");

            // Act
            string actual = insertable
                .Update(table)
                .Set(fieldName, "Say my name!")
                .Where(fieldId, 3)
                .GetQuery();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
