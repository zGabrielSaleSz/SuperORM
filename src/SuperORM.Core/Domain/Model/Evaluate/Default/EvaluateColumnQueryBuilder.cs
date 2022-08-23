using SuperORM.Core.Domain.Model.Evaluate.Interface;
using SuperORM.Core.Domain.Model.QueryBuilder;
using SuperORM.Core.Domain.Model.QueryBuilder.Fields;
using SuperORM.Core.Domain.Model.QueryBuilder.Fields.FieldsArgument;
using SuperORM.Core.Domain.Service.LinqSQL.SelectableTools;
using SuperORM.Core.Interface;
using System;

namespace SuperORM.Core.Domain.Model.Evaluate.Default
{
    internal class EvaluateColumnQueryBuilder<T> : IEvaluateColumn
    {
        private readonly IQuerySintax _querySintax;
        private readonly TableAssimilator _tableAssimilator;
        private readonly ColumnAssimilator _columnAssimilator;
        public EvaluateColumnQueryBuilder(TableAssimilator tableAssimilator, IQuerySintax querySintax, ColumnAssimilator columnAssimilator)
        {
            this._querySintax = querySintax;
            this._tableAssimilator = tableAssimilator;
            this._columnAssimilator = columnAssimilator;
        }

        public string GetEquivalentColumn(Type type, string propertyName)
        {
            string column = _columnAssimilator.GetByProperty(type, propertyName);
            Table table = _tableAssimilator.GetTableReference(type);
            IField field = new Field<Column>(table, column);
            return field.GetRaw(_querySintax);
        }
    }
}
