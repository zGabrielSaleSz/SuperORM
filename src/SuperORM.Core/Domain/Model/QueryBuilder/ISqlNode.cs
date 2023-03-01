using SuperORM.Core.Interface.Integration;

namespace SuperORM.Core.Domain.Model.QueryBuilder
{
    public interface ISqlNode<T>
    {
        T WithAlias(IQuerySintax querySintax);
    }
}
