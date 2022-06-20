using SuperORM.Core.Domain.Model.Sql;

namespace SuperORM.Core.Interface
{
    public interface IConnectionProvider
    {
        IConnection GetConnection(bool transacation = false);
        IBaseConnection GetBaseConnection();
        IQuerySintax GetQuerySintax();
    }
}
