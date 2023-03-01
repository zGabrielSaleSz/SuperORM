namespace SuperORM.Core.Interface.Integration
{
    public interface ITransactionConnection : IConnection
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
