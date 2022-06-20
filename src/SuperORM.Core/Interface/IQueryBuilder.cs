using SuperORM.Core.Domain.Model.QueryBuilder;

namespace SuperORM.Core.Interface.QueryBuilder
{
    public interface IQueryBuilder
    {
        string GetQuery();
        ParameterizedQuery GetQueryWithParameters();
    }
}
