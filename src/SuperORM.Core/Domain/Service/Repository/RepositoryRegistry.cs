using SuperORM.Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperORM.Core.Domain.Service.Repository
{
    public class RepositoryRegistry
    {
        private Dictionary<Type, Type> repositories;
        private static bool registryLodaded = false;
        private static RepositoryRegistry instance;
        private RepositoryRegistry()
        {
            repositories = new Dictionary<Type, Type>();
        }

        public static RepositoryRegistry GetInstance()
        {
            if (instance == null)
                instance = new RepositoryRegistry();
            return instance;
        }

        public IBaseRepository GetRepository(Type type)
        {
            return (IBaseRepository)Activator.CreateInstance(repositories[type]);
        }

        public void UseAllRepositories()
        {
            var typeBaseRepository = typeof(IBaseRepository);
            List<Type> response = AppDomain.CurrentDomain
                                .GetAssemblies()
                                .SelectMany(a => a.GetTypes())
                                .Where(p => typeBaseRepository.IsAssignableFrom(p)
                                    && !p.IsAbstract
                                    && !p.IsInterface)
                                .ToList();
            foreach (Type type in response)
            {

                repositories.Add(type.BaseType.GenericTypeArguments[0], type);
                //repositories.Add()
            }
            //var instaciated = (IBaseRepository)Activator.CreateInstance(testeInstancia);
        }

        public void AddRepository()
        {

        }
    }
}
