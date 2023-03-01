using SuperORM.Core.Interface.Integration;

namespace SuperORM.Core.Domain.Model.Sql
{
    public abstract class BaseConnectionProvider : IConnectionProvider
    {
        public IConnection GetNewConnection()
        {
            return new RepositoryConnection(this);
        }

        public ITransactionConnection GetNewTransaction()
        {
            return new SuperTransaction(this);
        }

        public abstract IBaseConnection GetBaseConnection();
        public abstract IQuerySintax GetQuerySintax();
    }
}
