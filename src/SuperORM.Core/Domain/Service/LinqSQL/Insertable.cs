using SuperORM.Core.Domain.Model.QueryBuilder;
using SuperORM.Core.Domain.Model.QueryBuilder.Insertable;
using SuperORM.Core.Domain.Model.Sql;
using SuperORM.Core.Domain.Service.Evaluator;
using SuperORM.Core.Domain.Service.LinqSQL.SelectableTools;
using SuperORM.Core.Domain.Service.QueryBuilder;
using SuperORM.Core.Interface.Integration;
using SuperORM.Core.Interface.LinqSQL;
using SuperORM.Core.Interface.QueryBuilder.SqlOperations;
using SuperORM.Core.Utilities.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SuperORM.Core.Domain.Service.LinqSQL
{
    public class Insertable<T> : IInsertable<T>
    {
        private readonly IConnection _connection;
        private readonly IInsertableBuilder _insertableBuilder;
        private readonly IQuerySintax _querySintax;
        private readonly List<string> _ignoredColumns;
        private readonly List<T> _valuesInsert;
        private ColumnAssimilator _columnAssimilator;

        public Insertable(IConnection connection, IQuerySintax querySintax)
        {
            _connection = connection;
            _querySintax = querySintax;
            _insertableBuilder = new InsertableBuilder(querySintax);
            _ignoredColumns = new List<string>();
            _valuesInsert = new List<T>();
            _columnAssimilator = ColumnAssimilator.Empty;
        }

        public IInsertable<T> AddColumnAssimilation(ColumnAssimilator columnAssimilation)
        {
            _columnAssimilator = columnAssimilation;
            return this;
        }

        public IInsertable<T> Into(string tableName)
        {
            _insertableBuilder.Into(tableName);
            return this;
        }

        public IInsertable<T> Ignore(params Expression<Func<T, object>>[] columnsExpressions)
        {
            foreach (var expression in columnsExpressions)
            {
                SqlExpressionEvaluator sqlEvaluator = new SqlExpressionEvaluator(expression.Body, _querySintax);
                Ignore(sqlEvaluator.Evaluate());
            }
            return this;
        }

        public IInsertable<T> Ignore(params string[] ignoredFields)
        {
            _ignoredColumns.AddRange(ignoredFields);
            return this;
        }

        public IInsertable<T> Values(params T[] values)
        {
            this._valuesInsert.AddRange(values);
            return this;
        }

        public IEnumerable<string> GetPropertiesInsert()
        {
            return ReflectionUtils.GetObjectProperties<T>().Where(p => !_ignoredColumns.Contains(p));
        }

        public string GetQuery()
        {
            AddValues();
            return _insertableBuilder.GetQuery();
        }

        public IInsertable<T> AddComplementRetrievePrimaryKey()
        {
            _insertableBuilder.AddComplementRetrievePrimaryKey();
            return this;
        }

        public ParameterizedQuery GetQueryWithParameters()
        {
            AddValues();
            return _insertableBuilder.GetQueryWithParameters();
        }

        private void AddValues()
        {
            foreach (T value in this._valuesInsert)
            {
                AddValue(value);
            }
            this._valuesInsert.Clear();
        }

        private void AddValue(T value)
        {
            Value newValue = new Value();
            ReflectionHandler<T> reflectionHandler = new ReflectionHandler<T>(value);
            foreach (string property in GetPropertiesInsert())
            {
                string respectiveColumn = _columnAssimilator.GetByProperty<T>(property);
                newValue.Add(respectiveColumn, reflectionHandler.GetPropertyValue(property));
            }
            _insertableBuilder.Values(newValue);
        }

        public int Execute()
        {
            ParameterizedQuery parameterizedQuery = GetQueryWithParameters();
            //IBaseConnection connection = _connectionProvider.GetConnection();

            CommandContext commandContext = new CommandContext();
            commandContext.AddQuery(parameterizedQuery);

            //IConnection repositoryConnection = new RepositoryConnection(connection);
            return _connection.ExecuteNonQuery(commandContext);
        }

        public TResult Execute<TResult>()
        {
            ParameterizedQuery parameterizedQuery = GetQueryWithParameters();
            //IBaseConnection connection = _connectionProvider.GetConnection();

            CommandContext commandContext = new CommandContext();
            commandContext.AddQuery(parameterizedQuery);

            //IConnection repositoryConnection = new RepositoryConnection(connection);
            ExecuteScalarResult<TResult> result = _connection.ExecuteScalar<TResult>(commandContext);

            return result.GetResult().FirstOrDefault().Value;
        }
    }
}
