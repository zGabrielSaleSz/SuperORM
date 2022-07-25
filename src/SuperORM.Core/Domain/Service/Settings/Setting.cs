using SuperORM.Core.Interface;

namespace SuperORM.Core.Domain.Service.Settings
{
    public class Setting
    {
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

        public Setting SetConnection(IConnectionProvider connection)
        {
            ConnectionProvider = connection;
            return this;
        }
    }
}
