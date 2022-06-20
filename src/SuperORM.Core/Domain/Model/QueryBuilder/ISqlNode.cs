using SuperORM.Core.Interface;

namespace SuperORM.Core.Domain.Model.QueryBuilder
{
    public interface ISqlNode<T>
    {
        T WithAlias(IQuerySintax querySintax);
    }
}
