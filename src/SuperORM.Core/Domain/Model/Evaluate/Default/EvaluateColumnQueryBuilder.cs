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
        public EvaluateColumnQueryBuilder(TableAssimilator tableAssimilator, IQuerySintax querySintax)
        {
            this._querySintax = querySintax;
            this._tableAssimilator = tableAssimilator;
        }

        public string GetEquivalentColumn(Type type, string propertyName)
        {
            Table table = _tableAssimilator.GetTableReference(type);
            IField field = new Field<Column>(table, propertyName);
            return field.GetRaw(_querySintax);
        }
    }
}
