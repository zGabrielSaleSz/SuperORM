using SuperORM.Core.Domain.Model.Evaluate.Default;
using SuperORM.Core.Domain.Model.Evaluate.Interface;
using SuperORM.Core.Domain.Model.QueryBuilder;
using SuperORM.Core.Domain.Model.QueryBuilder.Fields;
using SuperORM.Core.Domain.Model.QueryBuilder.Fields.FieldsArgument;
using SuperORM.Core.Domain.Model.Sql;
using SuperORM.Core.Domain.Service.LinqSQL.SelectableTools;
using SuperORM.Core.Domain.Service.QueryBuilder;
using SuperORM.Core.Interface;
using SuperORM.Core.Interface.LinqSQL;
using SuperORM.Core.Interface.QueryBuilder;
using System;
using System.Linq.Expressions;

namespace SuperORM.Core.Domain.Service.LinqSQL
{
    public class Updatable<T> : IUpdatable<T>
    {
        private readonly IConnection _connection;
        private readonly IUpdatableBuilder _updatableBuilder;
        private readonly IQuerySintax _querySintax;
        private readonly Table _table;
        private readonly ITableAssimilator _tableAssimilator;
        private ColumnAssimilator _columnAssimilator;
        public Updatable(IConnection connection, IQuerySintax querySintax)
        {
            _connection = connection;
            _updatableBuilder = new UpdatableBuilder(querySintax);
            _querySintax = querySintax;
            _table = new Table();
            _tableAssimilator = new TableAssimilator(typeof(T));
            _columnAssimilator = ColumnAssimilator.Empty;
        }

        public IUpdatable<T> AddColumnAssimilation(ColumnAssimilator columnAssimilation)
        {
            _columnAssimilator = columnAssimilation;
            return this;
        }

        public IUpdatable<T> Update(string tableName)
        {
            _table.Name = tableName;
            _tableAssimilator.SetMainTableName(_table);
            _updatableBuilder.Update(_table);
            return this;
        }

        public IUpdatable<T> Set<TResult>(System.Linq.Expressions.Expression<Func<T, TResult>> attribute, TResult value)
        {
            IField field = GetFieldByExpression(attribute);
            _updatableBuilder.Set(field, value);
            return this;
        }

        public IUpdatable<T> Set<TResult>(System.Linq.Expressions.Expression<Func<T, TResult>> attribute, System.Linq.Expressions.Expression<Func<T, TResult>> attribute2)
        {
            IField fieldOne = GetFieldByExpression(attribute);
            IField fieldTwo = GetFieldByExpression(attribute);
            _updatableBuilder.Set(fieldOne, fieldTwo);
            return this;
        }

        public IUpdatable<T> Set<TResult>(string attributeName, TResult value)
        {
            IField fieldOne = AddField(attributeName);
            _updatableBuilder.Set(fieldOne, value);
            return this;
        }

        public IUpdatable<T> Where(Expression<Func<T, bool>> expression)
        {
            IEvaluateColumn evaluateColumn = new EvaluateColumnQueryBuilder<T>(_tableAssimilator, _querySintax, _columnAssimilator);
            _updatableBuilder.SetWhereCondition(expression, evaluateColumn);
            return this;
        }

        public string GetQuery()
        {
            return _updatableBuilder.GetQuery();
        }

        public ParameterizedQuery GetQueryWithParameters()
        {
            return _updatableBuilder.GetQueryWithParameters();
        }

        private IField GetFieldByExpression<TResult>(Expression<Func<T, TResult>> attribute)
        {
            SqlExpressionEvaluator sqlEvaluator = new SqlExpressionEvaluator(attribute.Body, _querySintax);
            IField field = AddField(sqlEvaluator.Evaluate());
            return field;
        }

        public int Execute()
        {
            ParameterizedQuery parameterizedQuery = GetQueryWithParameters();

            CommandContext commandContext = new CommandContext();
            commandContext.AddQuery(parameterizedQuery);
            
            return _connection.ExecuteNonQuery(commandContext);
        }

        private IField AddField(string attributeName)
        {
            string respectiveColumn = _columnAssimilator.GetByProperty<T>(attributeName);
            return _table.AddField<Column>(respectiveColumn);
        }
    }
}
