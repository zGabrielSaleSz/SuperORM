using SuperORM.Core.Interface.Integration;

namespace SuperORM.Core.Domain.Model.QueryBuilder.Joins
{
    public interface IJoin
    {
        string GetRaw(IQuerySintax querySintax);
    }
}
