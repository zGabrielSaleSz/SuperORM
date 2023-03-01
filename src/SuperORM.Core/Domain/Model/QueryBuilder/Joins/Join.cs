using SuperORM.Core.Domain.Model.Common;
using SuperORM.Core.Domain.Model.QueryBuilder.Fields;
using SuperORM.Core.Interface.Integration;

namespace SuperORM.Core.Domain.Model.QueryBuilder.Joins
{
    public class Join : IJoin
    {
        private readonly IField fieldMainTable;
        private readonly IField fieldNewTable;
        public Join(IField fieldMainTable, IField fieldNewTable)
        {
            this.fieldMainTable = fieldMainTable;
            this.fieldNewTable = fieldNewTable;
        }
        public virtual string GetRaw(IQuerySintax querySintax)
        {
            return $"{SqlKeywords.JOIN} {fieldNewTable.GetTable().GetIdentifier(querySintax)} ON {fieldMainTable.GetFieldValue(querySintax)} = {fieldNewTable.GetFieldValue(querySintax)}";
        }
    }
}
