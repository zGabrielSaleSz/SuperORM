using SuperORM.Core.Domain.Model.QueryBuilder;
using System.Collections.Generic;

namespace SuperORM.Core.Interface
{
    public interface IBaseConnection
    {
        void OpenConnection();
        void OpenTransactionAttachedToConnection();
        int ExecuteNonQueryImplementation(ParameterizedQuery query);
        T ExecuteScalarImplementation<T>(ParameterizedQuery query);
        IEnumerable<IDictionary<string, object>> ExecuteReaderImplementation(ParameterizedQuery query);
        void Commit();
        void Rollback();
        void CloseTransactionAttachedToConnection();
        void CloseConnection();
    }
}
