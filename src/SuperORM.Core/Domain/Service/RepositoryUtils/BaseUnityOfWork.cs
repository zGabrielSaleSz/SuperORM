using SuperORM.Core.Interface.Integration;
using SuperORM.Core.Interface.Repository;

namespace SuperORM.Core.Domain.Service.Repository
{
    public class BaseUnityOfWork : IBaseUnityOfWork
    {
        private IRepositoryRegistry _repositoryRegistry;
        private IConnection _connection;
        private bool _useTransaction = false;
        public BaseUnityOfWork(IRepositoryRegistry repositoryRegistry)
        {
            _repositoryRegistry = repositoryRegistry;
        }

        public void UseTransaction()
        {
            _useTransaction = true;
            _connection = null;
        }

        public void Commit()
        {
            if (!_useTransaction)
                return;

            ITransactionConnection currentConnection = _connection as ITransactionConnection;
            currentConnection.Commit();
        }

        public void Rollback()
        {
            if (_useTransaction)
                return;

            ITransactionConnection currentConnection = _connection as ITransactionConnection;
            currentConnection.Rollback();
        }

        public T Get<T>() where T : IBaseRepository
        {
            LoadConnection();
            var repository = _repositoryRegistry.GetRepository<T>();
            if (_useTransaction == true)
            {
                repository.UseConnection(_connection);
            }
            return repository;
        }

        public void LoadConnection()
        {
            if (_connection != null)
                return;

            if (_useTransaction)
            {
                ApplyTransactionConnection();
            }
            else
            {
                ApplyConnection();
            }
        }

        private void ApplyConnection()
        {
            IConnection connection = GetConnectionProvider()
                .GetNewConnection();

            _connection = connection;
        }

        private void ApplyTransactionConnection()
        {
            ITransactionConnection transactionConnection = GetConnectionProvider()
                .GetNewTransaction();

            transactionConnection.BeginTransaction();
            _connection = transactionConnection;
        }

        private IConnectionProvider GetConnectionProvider()
        {
            return _repositoryRegistry.GetConnectionProvider();
        }
    }
}
