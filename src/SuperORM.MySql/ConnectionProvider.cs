using MySql.Data.MySqlClient;
using SuperORM.Core.Domain.Model.Sql;
using SuperORM.Core.Interface.Integration;

namespace SuperORM.MySql
{
    public class ConnectionProvider : BaseConnectionProvider, IConnectionProvider
    {
        private readonly string _connectionString;
        public ConnectionProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override IBaseConnection GetBaseConnection()
        {
            MySqlConnection connectionMySql = new MySqlConnection(this._connectionString);
            return new Connection(connectionMySql);
        }

        public override IQuerySintax GetQuerySintax()
        {
            return new QuerySintax();
        }
    }
}
