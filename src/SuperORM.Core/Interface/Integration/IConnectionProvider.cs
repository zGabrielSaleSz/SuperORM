namespace SuperORM.Core.Interface.Integration
{
    public interface IConnectionProvider
    {
        IConnection GetNewConnection();
        ITransactionConnection GetNewTransaction();
        IBaseConnection GetBaseConnection();
        IQuerySintax GetQuerySintax();
    }
}
