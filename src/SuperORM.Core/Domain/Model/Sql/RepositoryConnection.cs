using SuperORM.Core.Domain.Model.QueryBuilder;
using SuperORM.Core.Interface.Integration;
using System.Collections.Generic;

namespace SuperORM.Core.Domain.Model.Sql
{
    public class RepositoryConnection : IConnection
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly IBaseConnection _baseConnection;
        public RepositoryConnection(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
            _baseConnection = _connectionProvider.GetBaseConnection();
        }

        public int ExecuteNonQuery(CommandContext commandContext)
        {
            _baseConnection.OpenConnection();
            OpenTransaction(commandContext);
            int rowsChanged = 0;
            foreach (ParameterizedQuery query in commandContext.Queries)
            {
                rowsChanged += _baseConnection.ExecuteNonQueryImplementation(query);
            }
            CloseTransaction(commandContext);
            _baseConnection.CloseConnection();
            return rowsChanged;
        }

        public ExecuteScalarResult<T> ExecuteScalar<T>(CommandContext commandContext)
        {
            ExecuteScalarResult<T> result = new ExecuteScalarResult<T>();
            _baseConnection.OpenConnection();
            OpenTransaction(commandContext);
            foreach (ParameterizedQuery query in commandContext.Queries)
            {
                result.Add(query, _baseConnection.ExecuteScalarImplementation<T>(query));
            }
            CloseTransaction(commandContext);
            _baseConnection.CloseConnection();
            return result;
        }

        public IEnumerable<IDictionary<string, object>> ExecuteReader(CommandReaderContext commandContext)
        {
            _baseConnection.OpenConnection();
            foreach (var result in _baseConnection.ExecuteReaderImplementation(commandContext.Query))
            {
                yield return result;
            }
            _baseConnection.CloseConnection();
        }

        private void OpenTransaction(CommandContext commandContext)
        {
            if (commandContext.UseTransaction)
            {
                _baseConnection.OpenTransactionAttachedToConnection();
            }
        }

        private void CloseTransaction(CommandContext commandContext)
        {
            if (commandContext.UseTransaction)
            {
                _baseConnection.CloseTransactionAttachedToConnection();
            }
        }

        public void Dispose()
        {
            _baseConnection.CloseConnection();
        }
    }
}
