using SuperORM.Core.Domain.Model.QueryBuilder.Insertable;
using SuperORM.Core.Domain.Service.QueryBuilder;
using SuperORM.MySql;
using Xunit;

namespace SuperORM.Core.Test.QueryBuilder.Insertable
{
    public class InsertableBuilderMySqlTests
    {
        [Fact]
        public void Should_BuildQuery_When_Insertable()
        {
            //Arrange
            string expected = "INSERT INTO `users`(Nome, email, senha, ativo) VALUES('Gabriel', 'done@insert.com', 'seCuREpASSWORd', true), ('Gabriel', 'done@insert.com', 'seCuREpASSWORd', true)";
            InsertableBuilder insertable = new InsertableBuilder(new QuerySintax());

            Value value = new Value();
            value.Add("Nome", "Gabriel");
            value.Add("email", "done@insert.com");
            value.Add("senha", "seCuREpASSWORd");
            value.Add("ativo", true);

            Value value2 = new Value();
            value2.Add("Nome", "Gabriel");
            value2.Add("email", "done@insert.com");
            value2.Add("senha", "seCuREpASSWORd");
            value2.Add("ativo", true);

            // Act
            string actual = insertable
                .Into("users")
                .Values(value, value2)
                .GetQuery();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
