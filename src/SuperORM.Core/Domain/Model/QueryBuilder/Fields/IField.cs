using SuperORM.Core.Interface;

namespace SuperORM.Core.Domain.Model.QueryBuilder.Fields
{
    public interface IField
    {
        string GetFieldName();
        void SetTable(Table table);
        void SetAlias(string alias);
        string GetAlias();
        Table GetTable();
        string GetRaw(IQuerySintax querySintax);
        string GetFieldValue(IQuerySintax querySintax);
    }
}
