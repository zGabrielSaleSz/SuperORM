using SuperORM.Core.Domain.Service.Repository;
using SuperORM.Core.Interface.Repository;
using SuperORM.TestsResource.Repositories;

namespace SuperORM.WebAPI.Infrastructure.MySqlImp
{
    public class UnityOfWork : BaseUnityOfWork
    {
        public UnityOfWork(IRepositoryRegistry repositoryRegistry) : base(repositoryRegistry)
        {

        }
        public UserRepository Users => Get<UserRepository>();
        public DocumentRepository Documents => Get<DocumentRepository>();
    }
}
