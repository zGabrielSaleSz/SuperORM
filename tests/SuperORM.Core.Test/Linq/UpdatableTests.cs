using SuperORM.Core.Domain.Service.LinqSQL;
using SuperORM.Core.Domain.Service.LinqSQL.SelectableTools;
using SuperORM.Core.Interface.Integration;
using SuperORM.Core.Interface.LinqSQL;
using SuperORM.MySql;
using SuperORM.TestsResource.Entities;
using Xunit;

namespace SuperORM.Core.Test.Linq
{
    public class UpdatableTests
    {
        [Fact]
        public void Shold_BuildUpdate_WhenUsingUpdatable()
        {
            // Arrange
            string expected = "UPDATE `users` SET `users`.`active` = false " +
                              "WHERE (`users`.`id` = 5)";

            ConnectionProvider connectionProvider = new ConnectionProvider("");
            IQuerySintax querySintax = new QuerySintax();
            IUpdatable<User> updatable = new Updatable<User>(connectionProvider.GetNewConnection(), querySintax);
            updatable.Update("users").Set(u => u.active, false).Where(u => u.id == 5);

            // Act
            string actual = updatable.GetQuery();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Shold_BuildCustomFields_When_UsingColumnAssimilator()
        {
            // Arrange
            string expected = "UPDATE `users` SET `users`.`Active` = false " +
                              "WHERE (`users`.`ID` = 5)";

            ConnectionProvider connectionProvider = new ConnectionProvider("");
            IQuerySintax querySintax = new QuerySintax();

            ColumnAssimilator<User> columnAssimilator = new ColumnAssimilator<User>();
            columnAssimilator.Add("id", "ID");
            columnAssimilator.Add("active", "Active");
            IUpdatable<User> updatable = new Updatable<User>(connectionProvider.GetNewConnection(), querySintax);
            updatable.AddColumnAssimilation(columnAssimilator)
                .Update("users")
                .Set(u => u.active, false)
                .Where(u => u.id == 5);

            // Act
            string actual = updatable.GetQuery();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
