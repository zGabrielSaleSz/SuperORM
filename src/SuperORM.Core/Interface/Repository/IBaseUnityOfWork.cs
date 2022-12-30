namespace SuperORM.Core.Interface.Repository
{
    public interface IBaseUnityOfWork
    {
        void UseTransaction();
        void Commit();
        void Rollback();
    }
}
