using SuperORM.Core.Domain.Service.LinqSQL;
using SuperORM.Core.Interface;
using SuperORM.Core.Interface.LinqSQL;
using SuperORM.Core.Test.Complement.Model;
using SuperORM.MySql;
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
            IUpdatable<User> updatable = new Updatable<User>(connectionProvider.GetConnection(), querySintax);
            updatable.Update("users").Set(u => u.active, false).Where(u => u.id == 5);

            // Act
            string actual = updatable.GetQuery();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
