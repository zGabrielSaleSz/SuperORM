using MySql.Data.MySqlClient;
using SuperORM.Core.Domain.Model.QueryBuilder;
using SuperORM.Core.Domain.Model.Sql;
using SuperORM.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperORM.MySql
{
    public class Connection : IBaseConnection
    {
        private MySqlConnection _mySqlConnection;
        private MySqlTransaction _transaction;
        public Connection(MySqlConnection mySqlConnection)
        {
            _mySqlConnection = mySqlConnection;
        }

        public void OpenConnection()
        {
            _mySqlConnection.Open();
        }

        public void CloseConnection()
        {
            _mySqlConnection.Close();
        }

        public void OpenTransactionAttachedToConnection()
        {
            _transaction = _mySqlConnection.BeginTransaction();
        }

        public void CloseTransactionAttachedToConnection()
        {
            _transaction.Dispose();
        }

        public int ExecuteNonQueryImplementation(ParameterizedQuery query)
        {
            MySqlCommand command = BuildCommand(query);
            return command.ExecuteNonQuery();
        }

        public T ExecuteScalarImplementation<T>(ParameterizedQuery query)
        {
            MySqlCommand command = BuildCommand(query);
            return (T)Convert.ChangeType(command.ExecuteScalar(), typeof(T));
        }

        public IEnumerable<IDictionary<string, object>> ExecuteReaderImplementation(ParameterizedQuery query)
        {
            MySqlCommand command = BuildCommand(query);
            MySqlDataReader reader = command.ExecuteReader();
            foreach (IDictionary<string, object> result in Read(reader))
            {
                yield return result;
            }
        }

        private IEnumerable<IDictionary<string, object>> Read(MySqlDataReader reader)
        {
            if (!reader.HasRows)
                yield break;

            string[] columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToArray();
            while (reader.Read())
            {
                Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
                foreach (string column in columns)
                {
                    keyValuePairs[column] = reader[column];
                }
                yield return keyValuePairs;
            }
        }

        private MySqlCommand BuildCommand(ParameterizedQuery parameterizedQuery)
        {
            MySqlCommand command = new MySqlCommand(parameterizedQuery.Query, _mySqlConnection, _transaction);
            foreach (var parameter in parameterizedQuery.Parameters)
            {
                command.Parameters.AddWithValue($"@{parameter.Key}", parameter.Value);
            }
            return command;
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }
    }
}
