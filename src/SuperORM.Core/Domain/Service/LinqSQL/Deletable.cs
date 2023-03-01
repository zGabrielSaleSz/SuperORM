using SuperORM.Core.Domain.Evaluate.ColumnEvaluation;
using SuperORM.Core.Domain.Model.QueryBuilder;
using SuperORM.Core.Domain.Model.Sql;
using SuperORM.Core.Domain.Service.LinqSQL.SelectableTools;
using SuperORM.Core.Domain.Service.QueryBuilder;
using SuperORM.Core.Interface.Integration;
using SuperORM.Core.Interface.LinqSQL;
using SuperORM.Core.Interface.QueryBuilder.SqlOperations;
using System;
using System.Linq.Expressions;

namespace SuperORM.Core.Domain.Service.LinqSQL
{
    public class Deletable<T> : IDeletable<T>
    {
        private readonly IConnection _connection;
        private readonly IDeletableBuilder _deletableBuilder;
        private readonly IQuerySintax _querySintax;
        private readonly Table _table;
        private readonly TableAssimilator _tableAssimilator;
        private ColumnAssimilator _columnAssimilator;

        public Deletable(IConnection connection, IQuerySintax querySintax)
        {
            _connection = connection;
            _querySintax = querySintax;

            _table = new Table();
            _deletableBuilder = new DeletableBuilder(querySintax);
            _tableAssimilator = new TableAssimilator(typeof(T));
            _columnAssimilator = ColumnAssimilator.Empty;
        }

        public IDeletable<T> AddColumnAssimilation(ColumnAssimilator columnAssimilation)
        {
            _columnAssimilator = columnAssimilation;
            return this;
        }

        public IDeletable<T> From(string tableName)
        {
            _table.SetName(tableName);
            _tableAssimilator.SetMainTableName(_table);
            _deletableBuilder.From(_table);
            return this;
        }

        public IDeletable<T> Where(Expression<Func<T, bool>> expression)
        {
            IEvaluateColumn evaluateColumn = new EvaluateColumnQueryBuilder<T>(_tableAssimilator, _querySintax, _columnAssimilator);
            _deletableBuilder.SetWhereCondition(expression, evaluateColumn);
            return this;
        }

        public string GetQuery()
        {
            return _deletableBuilder.GetQuery();
        }

        public ParameterizedQuery GetQueryWithParameters()
        {
            return _deletableBuilder.GetQueryWithParameters();
        }

        public int Execute()
        {
            ParameterizedQuery parameterizedQuery = GetQueryWithParameters();

            CommandContext commandContext = new CommandContext();
            commandContext.AddQuery(parameterizedQuery);

            return _connection.ExecuteNonQuery(commandContext);
        }
    }
}
