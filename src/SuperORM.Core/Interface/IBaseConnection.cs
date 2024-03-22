using SuperORM.Core.Domain.Model.QueryBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperORM.Core.Interface
{
    public interface IBaseConnection
    {
        void OpenConnection();
        void OpenTransactionAttachedToConnection();
        bool IsConnectionOpen();
        int ExecuteNonQueryImplementation(ParameterizedQuery query);
        T ExecuteScalarImplementation<T>(ParameterizedQuery query);
        IEnumerable<IDictionary<string, object>> ExecuteReaderImplementation(ParameterizedQuery query);
        void Commit();
        void Rollback();
        void CloseTransactionAttachedToConnection();
        void CloseConnection();
    }
}
