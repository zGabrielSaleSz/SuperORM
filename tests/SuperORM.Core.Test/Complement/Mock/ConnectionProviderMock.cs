using SuperORM.Core.Domain.Model.Evaluate;
using SuperORM.Core.Domain.Model.QueryBuilder;
using SuperORM.Core.Domain.Model.Sql;
using SuperORM.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperORM.Core.Test.Complement.Mock
{

    public class Connection : IBaseConnection
    {
        public Connection()
        {

        }

        public void OpenConnection()
        {

        }

        public void CloseConnection()
        {

        }

        public void OpenTransactionAttachedToConnection()
        {

        }

        public void CloseTransactionAttachedToConnection()
        {
        }

        public int ExecuteNonQueryImplementation(ParameterizedQuery query)
        {
            return 0;
        }

        public T ExecuteScalarImplementation<T>(ParameterizedQuery query)
        {
            return default;
        }

        public IEnumerable<IDictionary<string, object>> ExecuteReaderImplementation(ParameterizedQuery query)
        {
            return default;
        }

        public void Commit()
        {
            
        }

        public void Rollback()
        {
            
        }
    }

    public class ConnectionProviderMock : BaseConnectionProvider, IConnectionProvider
    {
        public override IBaseConnection GetBaseConnection()
        {
            return new Connection();
        }

        public override IQuerySintax GetQuerySintax()
        {
            return new QuerySintaxDefault();
        }
    }
}
