using SuperORM.Core.Domain.Service.Settings;
using SuperORM.Core.Interface;
using SuperORM.Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperORM.Core.Domain.Service.Repository
{
    public class BaseUnityOfWork : IBaseUnityOfWork
    {
        private IConnection _connection;
        private bool _useTransaction = false;
        public BaseUnityOfWork()
        {

        }

        public void UseTransaction()
        {
            _useTransaction = true;
            _connection = null;
        }

        public void Commit()
        {
            if (_useTransaction)
            {
                ITransactionConnection currentConnection = _connection as ITransactionConnection;
                currentConnection.Commit();
            }
        }

        public void Rollback()
        {
            if (_useTransaction)
            {
                ITransactionConnection currentConnection = _connection as ITransactionConnection;

                currentConnection.Rollback();
            }
        }

        public T Get<T>() where T : IBaseRepository, new()
        {
            LoadConnection();
            var repository = new T();
            if (_useTransaction == true)
                repository.UseConnection(_connection);

            return repository;
        }

        public void LoadConnection()
        {
            if (_connection != null)
                return;

            if (_useTransaction)
            {
                ITransactionConnection transactionConnection = Setting.GetInstance().ConnectionProvider.GetNewTransaction();
                transactionConnection.BeginTransaction();
                _connection = transactionConnection;
            }
            else
            {
                _connection = Setting.GetInstance().ConnectionProvider.GetNewConnection();
            }
        }
    }
}
