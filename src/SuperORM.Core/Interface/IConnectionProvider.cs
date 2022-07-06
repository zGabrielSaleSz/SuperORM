using SuperORM.Core.Domain.Model.Sql;

namespace SuperORM.Core.Interface
{
    public interface IConnectionProvider
    {
        IConnection GetNewConnection();
        ITransactionConnection GetNewTransaction();
        IBaseConnection GetBaseConnection();
        IQuerySintax GetQuerySintax();
    }
}
