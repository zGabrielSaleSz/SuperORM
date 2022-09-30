using System;

namespace SuperORM.Core.Interface.Repository
{
    public interface IBaseRepository
    {
        void UseConnection(IConnection connection);
        void UseRepositoryRegistry(IRepositoryRegistry repositoryRegistry);
        string GetTableName();
        Type GetTargetType();
    }
}
