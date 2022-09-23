using SuperORM.Core.Domain.Service.Repository;
using SuperORM.Core.Interface;
using SuperORM.TestsResource.Entities;

namespace SuperORM.Core.Test.Complement.Mock
{
    internal class UserRepositoryNewImplementation : BaseRepository<User, int>
    {
        public UserRepositoryNewImplementation(IConnectionProvider connectionProvider) : base(connectionProvider)
        {

        }

        public override void Configurate()
        {
            SetTable("superUsers");
            SetPrimaryKey(u => u.id);
        }
    }
}
