using SuperORM.Core.Domain.Model.Repository;
using SuperORM.Core.Domain.Service.Repository;
using SuperORM.Core.Interface;
using SuperORM.TestsResource.Entities;

namespace SuperORM.TestsResource.Repositories
{
    public class NullTestsRepository : BaseRepository<NullTest, int>
    {
        public NullTestsRepository(IConnectionProvider connectionProvider) : base(connectionProvider)
        {

        }

        public override void Configurate()
        {
            SetTable("nullTests");
            SetPrimaryKey(n => n.id);
        }
        public override void ConfigurateColumns(IPropertyConfiguration<NullTest> propertyConfiguration)
        {
            base.ConfigurateColumns(propertyConfiguration);
        }

    }
}
