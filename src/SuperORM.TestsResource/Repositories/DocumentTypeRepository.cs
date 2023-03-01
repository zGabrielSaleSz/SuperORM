using SuperORM.Core.Domain.Service.Repository;
using SuperORM.Core.Interface.Integration;
using SuperORM.TestsResource.Entities;

namespace SuperORM.TestsResource.Repositories
{
    public class DocumentTypeRepository : BaseRepository<DocumentType, int>
    {
        public DocumentTypeRepository(IConnectionProvider connectionProvider) : base(connectionProvider)
        {

        }

        public override void Configurate()
        {
            SetTable("documentTypes");
            SetPrimaryKey(dt => dt.id);
        }
    }
}
