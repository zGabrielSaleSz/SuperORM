using SuperORM.Core.Domain.Model.Common;
using SuperORM.Core.Domain.Model.Evaluate.Interface;
using SuperORM.Core.Domain.Model.QueryBuilder;
using SuperORM.Core.Domain.Model.QueryBuilder.Insertable;
using SuperORM.Core.Domain.Model.QueryBuilder.Parameters;
using SuperORM.Core.Domain.Service.Evaluator;
using SuperORM.Core.Interface;
using SuperORM.Core.Interface.QueryBuilder;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperORM.Core.Domain.Service.QueryBuilder
{
    public class InsertableBuilder : IInsertableBuilder
    {
        private readonly Table _table;
        private readonly List<Value> _insertValues;
        private readonly IQuerySintax _querySintax;
        private readonly ParametersBuilder _parametersBuilder;
        private bool _retrievePrimaryKey = false;
        public InsertableBuilder(IQuerySintax querySintax)
        {
            _table = new Table(null);
            _insertValues = new List<Value>();
            _querySintax = querySintax;
            _parametersBuilder = new ParametersBuilder();
        }

        public IInsertableBuilder Into(string tableName)
        {
            _table.Name = tableName;
            return this;
        }

        public IInsertableBuilder Into(Table table)
        {
            this._table.Name = table.Name;
            this._table.Schema = table.Schema;
            this._table.Alias = table.Schema;
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
            stringBuilder.Append(GetInsertInto());
            stringBuilder.Append(GetColumns());
            stringBuilder.Append(GetValues());
            stringBuilder.Append(GetComplementRetrievePrimaryKey());
            return new ParameterizedQuery(stringBuilder.ToString().Trim(), _parametersBuilder.GetParameters());
        }

        private string GetComplementRetrievePrimaryKey()
        {
            if (!_retrievePrimaryKey)
                return string.Empty;
            return $"; {_querySintax.GetInsertComplementRetrievePrimaryKey()}";
        }

        private string GetInsertInto()
        {
            return $"{SqlKeywords.INSERT} {SqlKeywords.INTO} {_table.GetFullTableName(_querySintax)}";
        }

        private string GetColumns()
        {
            string[] columns = _insertValues.FirstOrDefault().GetColumnNames().ToArray();
            return $"({string.Join(SqlKeywords.COMMA, columns)})";
        }

        private string GetValues()
        {
            List<string> result = new List<string>();
            foreach (Value insertRegistry in _insertValues)
            {
                IEnumerable<string> columnNames = insertRegistry.GetColumnNames();
                List<string> fields = GetFieldsFromValue(insertRegistry, columnNames);
                result.Add($"({string.Join(SqlKeywords.COMMA, fields)})");
            }
            return $"{SqlKeywords.EMPTY_SPACE}{SqlKeywords.VALUES}{string.Join(SqlKeywords.COMMA, result)}";
        }

        private List<string> GetFieldsFromValue(Value insertRegistry, IEnumerable<string> columnNames)
        {
            List<string> fields = new List<string>(columnNames.Count());
            foreach (string column in columnNames)
            {
                IEvaluateResult data = CompiledValueEvaluator.Evaluate(_querySintax, insertRegistry.GetValue(column));
                IEvaluateParameter evaluateParameter = _parametersBuilder.Add(data, "insertableBuilder");
                fields.Add(evaluateParameter.GetValue());
            }
            return fields;
        }

        public IInsertableBuilder Values(params Value[] values)
        {
            _insertValues.AddRange(values);
            return this;
        }

        public IInsertableBuilder AddComplementRetrievePrimaryKey()
        {
            _retrievePrimaryKey = true;
            return this;
        }
    }
}
