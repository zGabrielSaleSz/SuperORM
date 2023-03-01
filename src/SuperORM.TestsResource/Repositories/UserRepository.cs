using SuperORM.Core.Domain.Model.Repository;
using SuperORM.Core.Domain.Service.Repository;
using SuperORM.Core.Interface.Integration;
using SuperORM.TestsResource.Entities;

namespace SuperORM.TestsResource.Repositories
{
    public class UserRepository : BaseRepository<User, int>
    {
        public UserRepository(IConnectionProvider connectionProvider) : base(connectionProvider)
        {

        }

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
