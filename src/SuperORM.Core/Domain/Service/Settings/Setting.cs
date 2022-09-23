using SuperORM.Core.Domain.Service.Repository;
using SuperORM.Core.Interface;

namespace SuperORM.Core.Domain.Service.Settings
{
    public class Setting
    {
        private RepositoryRegistry repositoryRegistry;
        public IConnectionProvider ConnectionProvider { get; private set; }
        private static Setting _instance;
        private Setting()
        {
            
        }

        public static Setting GetInstance()
        {
            if (_instance == null)
                _instance = new Setting();
            return _instance;
        }

        public RepositoryRegistry GetRepositoryRegistry()
        {
            if (repositoryRegistry == null)
                repositoryRegistry = new RepositoryRegistry(ConnectionProvider);
            return repositoryRegistry;
        }

        public Setting SetConnection(IConnectionProvider connection)
        {
            ConnectionProvider = connection;
            return this;
        }
    }
}
