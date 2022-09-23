using SuperORM.Core.Domain.Service.Repository;
using SuperORM.Core.Interface;
using SuperORM.TestsResource.Entities;

namespace SuperORM.TestsResource.Repositories
{
    public class DocumentRepository : BaseRepository<Document, int>
    {
        public DocumentRepository(IConnectionProvider connectionProvider) : base(connectionProvider)
        {

        }

        public override void Configurate()
        {
            SetTable("documents");
            SetPrimaryKey(d => d.id);
        }
    }
}
