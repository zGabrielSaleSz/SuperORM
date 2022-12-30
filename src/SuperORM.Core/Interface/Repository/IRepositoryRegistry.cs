using System;
using System.Reflection;

namespace SuperORM.Core.Interface.Repository
{
    public interface IRepositoryRegistry
    {
        IConnectionProvider GetConnectionProvider();
        IBaseRepository GetRepositoryOf<T>();
        T2 GetRepository<T2>()
            where T2 : IBaseRepository;
        IBaseRepository GetRepository(Type type);
        void UseAllRepositories(bool ignoreDuplicate, params Assembly[] assemblies);
        bool AddRepository<RepositoryType>()
            where RepositoryType : IBaseRepository;
    }
}
