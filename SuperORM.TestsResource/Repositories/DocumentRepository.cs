using SuperORM.Core.Domain.Service.Repository;
using SuperORM.TestsResource.Entities;

namespace SuperORM.TestsResource.Repositories
{
    public class DocumentRepository : BaseRepository<Document, int>
    {
        public override void Configurate()
        {
            SetTable("documents");
            SetPrimaryKey(d => d.id);
        }
    }
}
