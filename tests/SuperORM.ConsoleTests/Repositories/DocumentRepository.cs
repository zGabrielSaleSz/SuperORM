using SuperORM.Core.Domain.Service.Repository;
using SuperORM.Core.Interface;
using SuperORM.Core.Test.Complement.Model;

namespace SuperORM.ConsoleTests.Repositories
{
    public class DocumentRepository : BaseRepository<Document, int>
    {
        public override void Configurate()
        {
            SetTable("documents");
            SetPrimaryKey(d => d.id);
            SetRelationShip(d => d.idUser, d => d.user, u => u.id);
        }
    }
}
