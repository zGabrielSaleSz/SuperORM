using SuperORM.Core.Domain.Service.Repository;
using SuperORM.TestsResource.Entities;

namespace SuperORM.TestsResource.Repositories
{
    public class DocumentTypeRepository : BaseRepository<DocumentType, int>
    {
        public override void Configurate()
        {
            SetTable("documentTypes");
            SetPrimaryKey(dt => dt.id);
        }
    }
}
