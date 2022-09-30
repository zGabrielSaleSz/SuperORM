using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SuperORM.Core.Interface.Repository
{
    public interface IRepositoryRegistry
    {
        IBaseRepository GetRepositoryOf<T>();
        T2 GetRepository<T2>() 
            where T2 : IBaseRepository;
        IBaseRepository GetRepository(Type type);
        void UseAllRepositories(bool ignoreDuplicate, params Assembly[] assemblies);
        bool AddRepository<RepositoryType>()
            where RepositoryType : IBaseRepository;
    }
}
