using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using SuperORM.Core.Domain.Model.Sql;
using SuperORM.Core.Interface;

namespace SuperORM.MySql
{
    public class ConnectionProvider : BaseConnectionProvider, IConnectionProvider
    {
        private readonly MySqlConnection _connectionMySql;
        private readonly Connection _connection;

        public ConnectionProvider(string connectionString)
        {
            _connectionMySql = new MySqlConnection(connectionString);
            _connection = new Connection(_connectionMySql);
        }

        public ConnectionProvider(MySqlConnection mySqlConnection)
        {
            _connectionMySql = mySqlConnection;
            _connection = new Connection(_connectionMySql);
        }

        public override IBaseConnection GetBaseConnection()
        {
            return _connection;
        }

        public override IQuerySintax GetQuerySintax()
        {
            return new QuerySintax();
        }
    }
}
