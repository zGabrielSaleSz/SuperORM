using SuperORM.Core.Domain.Exceptions;
using SuperORM.Core.Domain.Model.Common;
using SuperORM.Core.Domain.Model.Enum;
using SuperORM.Core.Domain.Model.Evaluate.Interface;
using SuperORM.Core.Domain.Model.QueryBuilder;
using SuperORM.Core.Domain.Model.QueryBuilder.Fields;
using SuperORM.Core.Domain.Model.QueryBuilder.Fields.FieldsArgument;
using SuperORM.Core.Domain.Model.QueryBuilder.Joins;
using SuperORM.Core.Domain.Model.QueryBuilder.Parameters;
using SuperORM.Core.Domain.Model.QueryBuilder.Selectable;
using SuperORM.Core.Domain.Service.Adapters;
using SuperORM.Core.Domain.Service.Evaluator;
using SuperORM.Core.Interface;
using SuperORM.Core.Interface.QueryBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SuperORM.Core.Domain.Service.QueryBuilder
{
    public class SelectableBuilder : ISelectableBuilder
    {
        private readonly Table _table;
        private readonly IQuerySintax _querySintax;
        private readonly SqlPagination _pagination;

        private readonly List<IField> _selectFields;
        private readonly List<string> _whereConditions;
        private readonly List<Join> _joins;
        private readonly List<string> _groupByStatements;
        private readonly List<string> _havingStatements;
        private readonly List<string> _orderByStatements;

        private readonly WhereBuilder _whereBuilder;
        private readonly ParametersBuilder _parametersBuilder;

        public SelectableBuilder(IQuerySintax querySintax)
        {
            _table = new Table();
            _querySintax = querySintax;
            _whereConditions = new List<string>();
            _selectFields = new List<IField>();
            _joins = new List<Join>();
            _groupByStatements = new List<string>();
            _havingStatements = new List<string>();
            _orderByStatements = new List<string>();
            _pagination = new SqlPagination(_querySintax);
            _parametersBuilder = new ParametersBuilder();
            _whereBuilder = new WhereBuilder(_querySintax, _parametersBuilder);
        }

        public ISelectableBuilder Select(IFieldsBuilder fieldsBuilder)
        {
            return Select(fieldsBuilder.GetResult());
        }

        public ISelectableBuilder Select(IEnumerable<IField> fields)
        {
            return Select(fields.ToArray());
        }

        public ISelectableBuilder Select(params string[] fields)
        {
            Field<Column>[] columns = fields.Select(f => new Field<Column>(null, f)).ToArray();
            Select(columns);
            return this;
        }

        public ISelectableBuilder Select(params IField[] fields)
        {
            _selectFields.AddRange(fields);
            return this;
        }

        public ISelectableBuilder Top(uint rows)
        {
            if (!_querySintax.IsTopAvailable())
                throw new SqlDriverExpressionNotSupportedException();
            _pagination.Take(rows);
            return this;
        }

        public ISelectableBuilder From(Table table)
        {
            _table.CopyFrom(table);
            return this;
        }

        public ISelectableBuilder From(string table)
        {
            _table.Name = table;
            return this;
        }

        public void SetWhereCondition(string whereCondition)
        {
            _whereConditions.Add(whereCondition);
        }

        public IQuerySintax GetQuerySintax()
        {
            return _querySintax;
        }

        public ISelectableBuilder InnerJoin(IField field, IField field2)
        {
            _joins.Add(new InnerJoin(field, field2));
            return this;
        }

        public ISelectableBuilder LeftJoin(IField field, IField field2)
        {
            _joins.Add(new LeftJoin(field, field2));
            return this;
        }

        public ISelectableBuilder RightJoin(IField field, IField field2)
        {
            _joins.Add(new RightJoin(field, field2));
            return this;
        }

        public ISelectableBuilder CrossJoin(IField field, IField field2)
        {
            _joins.Add(new CrossJoin(field, field2));
            return this;
        }

        public ISelectableBuilder FullJoin(IField field, IField field2)
        {
            _joins.Add(new FullJoin(field, field2));
            return this;
        }

        public ISelectableBuilder SelfJoin(IField field, IField field2)
        {
            _joins.Add(new SelfJoin(field, field2));
            return this;
        }

        public ISelectableBuilder Limit(uint rows)
        {
            _pagination.Take(rows);
            return this;
        }

        public ISelectableBuilder Limit(uint startIndex, uint amount)
        {
            _pagination.Skip(startIndex);
            _pagination.Take(amount);
            return this;
        }

        public string GetQuery()
        {
            ParameterizedQuery queryWithParameters = GetQueryWithParameters();
            ParameterReplacer parameterReplacer = new ParameterReplacer(_querySintax, queryWithParameters.Query, _parametersBuilder);
            return parameterReplacer.GetResult();
        }

        public ParameterizedQuery GetQueryWithParameters()
        {
            FillDefaultTableInFields();

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(GetSelect());
            stringBuilder.Append(GetFrom());
            stringBuilder.Append(GetJoins());
            stringBuilder.Append(GetWhere());
            stringBuilder.Append(GetGroupBy());
            stringBuilder.Append(GetHaving());
            stringBuilder.Append(GetOrderBy());
            stringBuilder.Append(GetPagination());
            return new ParameterizedQuery(stringBuilder.ToString().Trim(), _parametersBuilder.GetParameters());
        }

        private void FillDefaultTableInFields()
        {
            var emptyTableFields = _selectFields.Where(f => f.GetTable() == null);
            foreach (IField field in emptyTableFields)
            {
                field.SetTable(_table);
            }
        }

        private string GetSelect()
        {
            IEnumerable<string> columns = _selectFields.Select(f => f.GetRaw(_querySintax));
            return $"{SqlKeywords.SELECT} {GetTop()}{string.Join(SqlKeywords.COMMA, columns)}";
        }

        private string GetTop()
        {
            return _pagination.GetTop();
        }

        private string GetFrom()
        {
            return $"{SqlKeywords.EMPTY_SPACE}{SqlKeywords.FROM} {_table.GetAsTableName(_querySintax)}";
        }

        private string GetJoins()
        {
            if (_joins.Count == 0)
                return string.Empty;

            return $"{SqlKeywords.EMPTY_SPACE}{string.Join(" ", _joins.Select(j => j.GetRaw(_querySintax)))}";
        }

        private string GetWhere()
        {
            if (_whereConditions.Count == 0)
                return string.Empty;

            return $"{SqlKeywords.EMPTY_SPACE}{SqlKeywords.WHERE} {string.Join($" {SqlKeywords.AND} ", _whereConditions)}";
        }

        private string GetGroupBy()
        {
            if (_groupByStatements.Count == 0)
                return string.Empty;

            return $"{SqlKeywords.EMPTY_SPACE}{SqlKeywords.GROUP_BY} {string.Join(SqlKeywords.COMMA, _groupByStatements)}";
        }

        private string GetHaving()
        {
            if (_havingStatements.Count == 0)
                return string.Empty;

            return $"{SqlKeywords.EMPTY_SPACE}{SqlKeywords.HAVING} {string.Join(SqlKeywords.COMMA, _havingStatements)}";
        }

        private string GetOrderBy()
        {
            if (_orderByStatements.Count == 0)
                return string.Empty;

            return $"{SqlKeywords.EMPTY_SPACE}{SqlKeywords.ORDER_BY} {string.Join(SqlKeywords.COMMA, _orderByStatements)}";
        }

        private string GetPagination()
        {
            string paginationString = _pagination.GetPagination();
            if (string.IsNullOrWhiteSpace(paginationString))
                return string.Empty;
            return $"{SqlKeywords.EMPTY_SPACE}{paginationString}";
        }

        public ISelectableBuilder Where(IField field, IField field2)
        {
            Where(field, SqlComparator.Equal, field2);
            return this;
        }

        public ISelectableBuilder Where<T>(IField field, T value)
        {
            Where(field, SqlComparator.Equal, value);
            return this;
        }

        public ISelectableBuilder Where<T>(IField field, SqlComparator sqlOperator, T value)
        {
            _whereConditions.Add($"({_whereBuilder.Build(field, sqlOperator, value)})");
            return this;
        }

        public ISelectableBuilder Where(IField field, SqlComparator sqlOperator, IField field2)
        {
            _whereConditions.Add($"({_whereBuilder.Build(field, sqlOperator, field2)})");
            return this;
        }

        public ISelectableBuilder GroupBy(params IField[] fields)
        {
            _groupByStatements.AddRange(fields.Select(f => f.GetFieldValue(_querySintax)));
            return this;
        }

        public ISelectableBuilder Having<T>(IField field, SqlComparator sqlOperator, T value)
        {
            _havingStatements.Add($"({field.GetFieldValue(_querySintax)} {GetUsualComparator(sqlOperator)} {CompiledValueEvaluator.Evaluate(_querySintax, value).GetValue()})");
            return this;
        }

        public ISelectableBuilder OrderBy(params IField[] fields)
        {
            _orderByStatements.AddRange(fields.Select(f => $"{f.GetFieldValue(_querySintax)} {SqlKeywords.ASC}"));
            return this;
        }

        public ISelectableBuilder OrderByDescending(params IField[] fields)
        {
            _orderByStatements.AddRange(fields.Select(f => $"{f.GetFieldValue(_querySintax)} {SqlKeywords.DESC}"));
            return this;
        }

        public ISelectableBuilder Skip(uint rows)
        {
            _pagination.Skip(rows);
            return this;
        }

        public ISelectableBuilder Take(uint rows)
        {
            _pagination.Take(rows);
            return this;
        }

        private static string GetUsualComparator(SqlComparator sqlOperator)
        {
            return SqlHandler.GetByNodeType(ExpressionTypeAdapter.GetExpressionType(sqlOperator));
        }

        public bool HasGroupBy()
        {
            return _groupByStatements.Any();
        }

        public bool HasJoin()
        {
            return _joins.Any();
        }

        public bool HasOrderBy()
        {
            return _orderByStatements.Any();
        }

        public bool HasWhere()
        {
            return _whereConditions.Any();
        }

        public bool HasLimit()
        {
            return !_pagination.IsEmpty();
        }

        public bool HasSelect()
        {
            return _selectFields.Any();
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
