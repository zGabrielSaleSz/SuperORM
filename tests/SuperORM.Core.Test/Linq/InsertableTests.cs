using SuperORM.Core.Domain.Service.LinqSQL;
using SuperORM.Core.Interface;
using SuperORM.Core.Interface.LinqSQL;
using SuperORM.Core.Test.Complement.Model;
using SuperORM.MySql;
using Xunit;

namespace SuperORM.Core.Test.Linq
{
    public class InsertableTests
    {
        [Fact]
        public void Shold_BuildInsert_WhenUsingInsertable()
        {
            // Arrange
            string expected = "INSERT INTO `users`(id, Name, email, password, active, approvedDate) " +
                              "VALUES(26, 'Gabriel Sales', 'gabriel@superorm.com', 'SuperSecretPassW0rd!', false, '2019-05-10 22:05:19')";

            ConnectionProvider connectionProvider = new ConnectionProvider("");
            IQuerySintax querySintax = connectionProvider.GetQuerySintax();
            IInsertable<User> insertable = new Insertable<User>(connectionProvider.GetNewConnection(), querySintax);

            User user = new User();
            user.id = 26;
            user.Name = "Gabriel Sales";
            user.email = "gabriel@superorm.com";
            user.password = "SuperSecretPassW0rd!";
            user.active = false;
            user.approvedDate = new System.DateTime(2019, 05, 10, 22, 05, 19);

            insertable.Into("users").Values(user);

            // Act
            string actual = insertable.GetQuery();

            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Shold_IgnoreFields_WhenUsingInsertableWithIgnore()
        {
            // Arrange
            string expected = "INSERT INTO `users`(Name, email, password, active) " +
                              "VALUES('Gabriel Sales', 'gabriel@superorm.com', 'SuperSecretPassW0rd!', false)";

            ConnectionProvider connectionProvider = new ConnectionProvider("");
            IQuerySintax querySintax = new QuerySintax();
            IInsertable<User> insertable = new Insertable<User>(connectionProvider.GetNewConnection(), querySintax);

            User user = new User();
            user.Name = "Gabriel Sales";
            user.email = "gabriel@superorm.com";
            user.password = "SuperSecretPassW0rd!";
            user.active = false;

            insertable.Into("users").Values(user)
                .Ignore(u => u.id)
                .Ignore(u => u.approvedDate);

            // Act
            string actual = insertable.GetQuery();

            // Assert
            Assert.Equal(expected, actual);

        }
    }
}
