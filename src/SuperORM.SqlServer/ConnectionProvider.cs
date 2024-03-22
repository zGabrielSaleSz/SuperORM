using SuperORM.Core.Domain.Model.Sql;
using SuperORM.Core.Interface;
using System.Data.SqlClient;

namespace SuperORM.SqlServer
{
    public class ConnectionProvider : BaseConnectionProvider, IConnectionProvider
    {
        private readonly SqlConnection _connectionSqlServer;

        public ConnectionProvider(string connectionString)
        {
            _connectionSqlServer = new SqlConnection(connectionString);
        }

        public ConnectionProvider(SqlConnection connectionSqlServer)
        {
            _connectionSqlServer = connectionSqlServer;
        }

        public override IBaseConnection GetBaseConnection()
        {
            return new Connection(_connectionSqlServer);
        }

        public override IQuerySintax GetQuerySintax()
        {
            return new QuerySintax();
        }
    }
}
