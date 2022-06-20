using SuperORM.Core.Domain.Model.QueryBuilder;
using SuperORM.Core.Domain.Model.Sql;
using SuperORM.Core.Interface;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SuperORM.SqlServer
{
    public class Connection : IBaseConnection
    {
        private readonly SqlConnection _sqlConnection;
        private SqlTransaction _transaction;
        public Connection(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public void OpenConnection()
        {
            _sqlConnection.Open();
        }

        public void CloseConnection()
        {
            _sqlConnection.Close();
        }

        public void OpenTransactionAttachedToConnection()
        {
            _transaction = _sqlConnection.BeginTransaction();
        }

        public void CloseTransactionAttachedToConnection()
        {
            _transaction.Dispose();
        }

        public int ExecuteNonQueryImplementation(ParameterizedQuery query)
        {
            SqlCommand command = BuildCommand(query);
            return command.ExecuteNonQuery();
        }

        public T ExecuteScalarImplementation<T>(ParameterizedQuery query)
        {
            SqlCommand command = BuildCommand(query);
            return (T)command.ExecuteScalar();
        }

        public IEnumerable<IDictionary<string, object>> ExecuteReaderImplementation(ParameterizedQuery query)
        {
            SqlCommand command = BuildCommand(query);
            SqlDataReader reader = command.ExecuteReader();
            foreach (var result in Read(reader))
            {
                yield return result;
            }
        }

        private IEnumerable<IDictionary<string, object>> Read(SqlDataReader reader)
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

        private SqlCommand BuildCommand(ParameterizedQuery parameterizedQuery)
        {
            SqlCommand command = new SqlCommand(parameterizedQuery.Query, _sqlConnection, _transaction);
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
