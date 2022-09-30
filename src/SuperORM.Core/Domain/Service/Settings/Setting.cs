using SuperORM.Core.Domain.Service.Repository;
using SuperORM.Core.Interface;
using SuperORM.Core.Interface.Repository;
using System;
using System.Collections.Generic;

namespace SuperORM.Core.Domain.Service.Settings
{
    public class Setting
    {
        private Dictionary<IComparable, IRepositoryRegistry> RepositoryRegisters;
        private RepositoryRegistry repositoryRegistry;
        public IConnectionProvider ConnectionProvider { get; private set; }
        private static Setting _instance;
        private Setting()
        {
            RepositoryRegisters = new Dictionary<IComparable, IRepositoryRegistry>();
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

        public void SetRepositoryRegistry<T>(T key, IRepositoryRegistry repositoryRegistry) where T : IComparable
        {
            if (RepositoryRegisters.ContainsKey(key))
                throw new Exceptions.DuplicatedRepositoryRegistry();
            RepositoryRegisters.Add(key, repositoryRegistry);
        }

        public IRepositoryRegistry GetRepositoryRegistry<T>(T key) where T : IComparable
        {
            if (!RepositoryRegisters.ContainsKey(key))
                throw new Exceptions.NotFoundedRepositoryRegistry();
            return RepositoryRegisters[key];
        }
    }
}
