using SuperORM.Core.Interface;

namespace SuperORM.Core.Domain.Model.QueryBuilder.Fields
{
    public interface IField
    {
        public string GetFieldName();
        public void SetTable(Table table);
        public Table GetTable();
        public string GetRaw(IQuerySintax querySintax);
        public string GetFieldValue(IQuerySintax querySintax);
    }
}
