using SuperORM.Core.Domain.Model.QueryBuilder;
using SuperORM.Core.Domain.Model.QueryBuilder.Insertable;

namespace SuperORM.Core.Interface.QueryBuilder.SqlOperations
{
    public interface IInsertableBuilder : IQueryBuilder
    {
        IInsertableBuilder Into(string table);
        IInsertableBuilder Into(Table table);
        IInsertableBuilder Values(params Value[] values);
        IInsertableBuilder AddComplementRetrievePrimaryKey();
    }
}
