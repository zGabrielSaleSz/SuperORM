using SuperORM.Core.Interface;

namespace SuperORM.Core.Domain.Model.QueryBuilder.Joins
{
    public interface IJoin
    {
        string GetRaw(IQuerySintax querySintax);
    }
}
