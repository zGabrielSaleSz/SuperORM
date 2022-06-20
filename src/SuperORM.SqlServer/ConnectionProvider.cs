using SuperORM.Core.Domain.Model.Sql;
using SuperORM.Core.Interface;
using System.Data.SqlClient;

namespace SuperORM.SqlServer
{
    public class ConnectionProvider : BaseConnectionProvider, IConnectionProvider
    {
        private readonly string _connectionString;
        public ConnectionProvider(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public override IBaseConnection GetBaseConnection()
        {
            SqlConnection connectionSqlServer = new SqlConnection(this._connectionString);
            return new Connection(connectionSqlServer);
        }

        public override IQuerySintax GetQuerySintax()
        {
            return new QuerySintax();
        }
    }
}
