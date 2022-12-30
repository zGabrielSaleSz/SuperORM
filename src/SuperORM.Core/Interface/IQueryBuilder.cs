using SuperORM.Core.Domain.Model.QueryBuilder;

namespace SuperORM.Core.Interface
{
    public interface IQueryBuilder
    {
        string GetQuery();
        ParameterizedQuery GetQueryWithParameters();
    }
}
