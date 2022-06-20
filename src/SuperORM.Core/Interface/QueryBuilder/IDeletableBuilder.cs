using SuperORM.Core.Domain.Model.QueryBuilder;

namespace SuperORM.Core.Interface.QueryBuilder
{
    public interface IDeletableBuilder : IWhereable<IDeletableBuilder>, IQueryBuilder
    {
        IDeletableBuilder From(Table table);
        IDeletableBuilder From(string table);
    }
}
