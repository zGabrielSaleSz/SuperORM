using SuperORM.Core.Domain.Model.Repository;
using SuperORM.Core.Domain.Service.Repository;
using SuperORM.Core.Interface;
using SuperORM.Core.Test.Complement.Model;
using System.Transactions;

namespace SuperORM.ConsoleTests.Repositories
{
    public class UserRepository : BaseRepository<User, int>
    {
        public override void Configurate()
        {
            SetTable("users");
            SetPrimaryKey(u => u.id);
        }

        public override void ConfigurateColumns(IPropertyConfiguration<User> propertyConfiguration)
        {
            propertyConfiguration.SetColumnName(u => u.Name, "name");
        }
    }
}
