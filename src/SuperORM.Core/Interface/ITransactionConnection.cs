namespace SuperORM.Core.Interface
{
    public interface ITransactionConnection : IConnection
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
