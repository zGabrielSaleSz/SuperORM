using SuperORM.Core.Domain.Evaluate.ColumnEvaluation;
using SuperORM.Core.Domain.Model.Common;
using SuperORM.Core.Domain.Model.Enum;
using SuperORM.Core.Domain.Model.QueryBuilder;
using SuperORM.Core.Domain.Model.QueryBuilder.Fields;
using SuperORM.Core.Domain.Model.QueryBuilder.Parameters;
using SuperORM.Core.Domain.Service.Evaluator;
using SuperORM.Core.Interface.Integration;
using SuperORM.Core.Interface.QueryBuilder.SqlOperations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SuperORM.Core.Domain.Service.QueryBuilder
{
    public class DeletableBuilder : IDeletableBuilder
    {
        private readonly IQuerySintax _querySintax;
        private readonly Table _table;
        private readonly List<string> _whereConditions;

        private readonly ParametersBuilder _parametersBuilder;
        private readonly WhereBuilder _whereBuilder;

        public DeletableBuilder(IQuerySintax querySintax)
        {
            _table = new Table();
            _querySintax = querySintax;
            _whereConditions = new List<string>();
            _parametersBuilder = new ParametersBuilder();
            _whereBuilder = new WhereBuilder(_querySintax, _parametersBuilder);
        }

        public IDeletableBuilder From(Table table)
        {
            this._table.CopyFrom(table);
            return this;
        }

        public IDeletableBuilder From(string table)
        {
            this._table.SetName(table);
            return this;
        }

        public IDeletableBuilder Where(IField field, IField field2)
        {
            Where(field, SqlComparator.Equal, field2);
            return this;
        }

        public IDeletableBuilder Where<T>(IField field, T value)
        {
            Where(field, SqlComparator.Equal, value);
            return this;
        }

        public IDeletableBuilder Where<T>(IField field, SqlComparator sqlOperator, T value)
        {
            _whereConditions.Add($"({_whereBuilder.Build(field, sqlOperator, value)})");
            return this;
        }

        public IDeletableBuilder Where(IField field, SqlComparator sqlOperator, IField field2)
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
            stringBuilder.Append(GetDelete());
            stringBuilder.Append(GetCondition());
            return new ParameterizedQuery(stringBuilder.ToString().Trim(), _parametersBuilder.GetParameters());
        }

        private string GetDelete()
        {
            return $"{SqlKeywords.DELETE} {SqlKeywords.FROM} {_table.GetFullTableName(_querySintax)}";
        }

        private string GetCondition()
        {
            if (_whereConditions.Count == 0)
                return string.Empty;
            return $"{SqlKeywords.EMPTY_SPACE}{SqlKeywords.WHERE} {string.Join($" {SqlKeywords.AND} ", _whereConditions)}";
        }

        public void SetWhereCondition<T>(Expression<Func<T, bool>> expression, IEvaluateColumn evaluateColumn)
        {
            var sqlExpression = new SqlExpressionEvaluator(expression.Body, _querySintax);
            sqlExpression.EvaluateContext.SetColumnEvaluator(evaluateColumn);
            sqlExpression.EvaluateContext.SetParametersBuilder(_parametersBuilder);
            _whereConditions.Add(sqlExpression.EvaluateWithParameters());
        }
    }
}
