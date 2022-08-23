namespace SuperORM.Core.Interface.Repository
{
    public interface IBaseRepository
    {
        void UseConnection(IConnection connection);
    }
}
