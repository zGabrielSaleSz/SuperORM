using SuperORM.Core.Domain.Model.QueryBuilder;
using SuperORM.Core.Domain.Service.Repository;
using SuperORM.Core.Interface;
using SuperORM.Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperORM.Core.Domain.Model.Sql
{
    public class SuperTransaction : IConnection, IDisposable
    {
        private readonly IBaseConnection _transactionConnection;
        private bool _finishedConnection = false;
        public SuperTransaction(IConnectionProvider connectionProvider)
        {
            _transactionConnection = connectionProvider.GetBaseConnection();
        }

        public void BeginTransaction()
        {
            _transactionConnection.OpenConnection();
            _transactionConnection.OpenTransactionAttachedToConnection();
        }

        public T Use<T>() where T : IBaseRepository, new()
        {
            T repository = new T();
            repository.UseConnection(this);
            return repository;
        }

        public void Commit()
        {
            if (_finishedConnection)
                return;

            _transactionConnection.Commit();
            FinishTransaction();
        }

        public void Rollback()
        {
            if (_finishedConnection)
                return;

            _transactionConnection.Rollback();
            FinishTransaction();
        }

        private void FinishTransaction()
        {
            _finishedConnection = true;
            _transactionConnection.CloseTransactionAttachedToConnection();
            _transactionConnection.CloseConnection();
        }

        public void Dispose()
        {
            Commit();
        }

        public int ExecuteNonQuery(CommandContext commandContext)
        {
            int rowsChanged = 0;
            foreach(ParameterizedQuery parameterizedQuery in commandContext.Queries)
            {
                rowsChanged += _transactionConnection.ExecuteNonQueryImplementation(parameterizedQuery);
            }
            return rowsChanged;
        }

        public ExecuteScalarResult<T> ExecuteScalar<T>(CommandContext commandContext)
        {
            ExecuteScalarResult<T> result = new ExecuteScalarResult<T>();
            foreach (ParameterizedQuery parameterizedQuery in commandContext.Queries)
            {
                result.Add(parameterizedQuery, _transactionConnection.ExecuteScalarImplementation<T>(parameterizedQuery)); 
            }
            return result;
        }

        public IEnumerable<IDictionary<string, object>> ExecuteReader(CommandReaderContext readerContext)
        {
            return _transactionConnection.ExecuteReaderImplementation(readerContext.Query);
        }
    }
}
