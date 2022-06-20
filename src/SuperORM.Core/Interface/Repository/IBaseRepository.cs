namespace SuperORM.Core.Interface.Repository
{
    public interface IBaseRepository
    {
        public void UseConnection(IConnection connection);
    }
}
