using SuperORM.Core.Domain.Evaluate.ColumnEvaluation;
using SuperORM.Core.Domain.Model.Common;
using SuperORM.Core.Domain.Model.Enum;
using SuperORM.Core.Domain.Model.QueryBuilder;
using SuperORM.Core.Domain.Model.QueryBuilder.Fields;
using SuperORM.Core.Domain.Model.QueryBuilder.Parameters;
using SuperORM.Core.Domain.Service.Evaluator;
using SuperORM.Core.Interface;
using SuperORM.Core.Interface.QueryBuilder;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SuperORM.Core.Domain.Service.QueryBuilder
{
    public class UpdatableBuilder : IUpdatableBuilder
    {
        private readonly Table _table;
        private readonly IQuerySintax _querySintax;
        private readonly List<string> _setStatements;
        private readonly List<string> _whereConditions;

        private readonly WhereBuilder _whereBuilder;
        private readonly ParametersBuilder _parametersBuilder;

        public UpdatableBuilder(IQuerySintax querySintax)
        {
            _querySintax = querySintax;
            _table = new Table(null);
            _setStatements = new List<string>();
            _whereConditions = new List<string>();
            _parametersBuilder = new ParametersBuilder();
            _whereBuilder = new WhereBuilder(_querySintax, _parametersBuilder);
        }

        public IUpdatableBuilder Update(string tableName)
        {
            _table.SetName(tableName);
            return this;
        }

        public IUpdatableBuilder Update(Table table)
        {
            _table.CopyFrom(table);
            return this;
        }

        public IUpdatableBuilder Set(IField field, IField field2)
        {
            _setStatements.Add(_whereBuilder.Build(field, SqlComparator.Equal, field2));
            return this;
        }

        public IUpdatableBuilder Set<T>(IField field, T value)
        {
            _setStatements.Add(_whereBuilder.Build(field, SqlComparator.Equal, value));
            return this;
        }

        public IUpdatableBuilder Where(IField field, IField field2)
        {
            Where(field, SqlComparator.Equal, field2);
            return this;
        }

        public IUpdatableBuilder Where<T>(IField field, T value)
        {
            Where(field, SqlComparator.Equal, value);
            return this;
        }

        public IUpdatableBuilder Where<T>(IField field, SqlComparator sqlOperator, T value)
        {
            _whereConditions.Add($"({_whereBuilder.Build(field, sqlOperator, value)})");
            return this;
        }

        public IUpdatableBuilder Where(IField field, SqlComparator sqlOperator, IField field2)
        {
            _whereConditions.Add($"({_whereBuilder.Build(field, sqlOperator, field2)})");
            return this;
        }

        public string GetQuery()
        {
            ParameterizedQuery parameterizedQuery = GetQueryWithParameters();
            ParameterReplacer parameterReplacer = new ParameterReplacer(_querySintax, parameterizedQuery.Query, _parametersBuilder);
            return parameterReplacer.GetResult();
        }

        public ParameterizedQuery GetQueryWithParameters()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(GetUpdate());
            stringBuilder.Append(GetSetStatements());
            stringBuilder.Append(GetCondition());
            return new ParameterizedQuery(stringBuilder.ToString().Trim(), _parametersBuilder.GetParameters());
        }

        private string GetUpdate()
        {
            return $"{SqlKeywords.UPDATE} {_table.GetFullTableName(_querySintax)}";
        }

        private string GetSetStatements()
        {
            return $"{SqlKeywords.EMPTY_SPACE}{SqlKeywords.SET} {string.Join(SqlKeywords.COMMA, _setStatements)}";
        }

        private string GetCondition()
        {
            if (_whereConditions.Count == 0)
                return string.Empty;
            return $"{SqlKeywords.EMPTY_SPACE}{SqlKeywords.WHERE} {string.Join($" {SqlKeywords.AND} ", _whereConditions)}";
        }

        public void SetWhereCondition<T>(Expression<Func<T, bool>> expression, IEvaluateColumn evaluateColumn)
        {
            SqlExpressionEvaluator sqlExpression = new SqlExpressionEvaluator(expression.Body, _querySintax);
            sqlExpression.EvaluateContext.SetColumnEvaluator(evaluateColumn);
            sqlExpression.EvaluateContext.SetParametersBuilder(_parametersBuilder);
            _whereConditions.Add(sqlExpression.EvaluateWithParameters());
        }
    }
}
