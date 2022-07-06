using SuperORM.Core.Domain.Service.LinqSQL;
using SuperORM.Core.Interface;
using SuperORM.Core.Interface.LinqSQL;
using SuperORM.Core.Test.Complement.Model;
using SuperORM.MySql;
using Xunit;

namespace SuperORM.Core.Test.Linq
{
    public class DeletableTests
    {
        [Fact]
        public void Shold_BuildDelete_WhenUsingDeletable()
        {
            // Arrange
            string expected = "DELETE FROM `users` " +
                              "WHERE (`users`.`id` = 5)";

            ConnectionProvider connectionProvider = new ConnectionProvider("");
            IQuerySintax querySintax = new QuerySintax();
            IDeletable<User> deletable = new Deletable<User>(connectionProvider.GetNewConnection(), querySintax);
            deletable.From("users").Where(u => u.id == 5);

            // Act
            string actual = deletable.GetQuery();

            // Assert
            Assert.Equal(expected, actual);

        }
    }
}
