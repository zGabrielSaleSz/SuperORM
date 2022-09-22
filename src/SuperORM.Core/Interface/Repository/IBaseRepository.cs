using System;

namespace SuperORM.Core.Interface.Repository
{
    public interface IBaseRepository
    {
        void UseConnection(IConnection connection);
        string GetTableName();
        Type GetTargetType();
    }
}
