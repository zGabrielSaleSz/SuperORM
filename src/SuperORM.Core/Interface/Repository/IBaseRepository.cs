namespace SuperORM.Core.Interface.Repository
{
    public interface IBaseRepository
    {
        void UseConnection(IConnection connection);
        void UseConnectionProvider(IConnectionProvider connectionProvider);
        string GetTableName();
    }
}
