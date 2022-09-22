using SuperORM.Core.Domain.Exceptions;
using SuperORM.Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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

        public IBaseRepository GetRepository<T>()
        {
            return GetRepository(typeof(T));
        }

        public IBaseRepository GetRepository(Type type)
        {
            if (!repositories.ContainsKey(type))
                throw new EntityNotConfiguredException($"Repository could not be found for '{type.AssemblyQualifiedName}'");
            return (IBaseRepository)Activator.CreateInstance(repositories[type]);
        }

        public void UseAllRepositories(bool doNotIncludeDuplicates = false)
        {
            var typeBaseRepository = typeof(IBaseRepository);
            List<Type> response = AppDomain.CurrentDomain
                                .GetAssemblies()
                                .SelectMany(a => a.GetTypes())
                                .Where(p => typeBaseRepository.IsAssignableFrom(p)
                                    && !p.IsAbstract
                                    && !p.IsInterface)
                                .ToList();

            if(doNotIncludeDuplicates)
            {
                response = response
                    .GroupBy(r => GetEntity(r))
                    .ToDictionary(k => k, v => v.Count())
                    .Where(v => v.Value == 1)
                    .SelectMany(r => r.Key)
                    .ToList();
            }
            else
            {
                Dictionary<Type, Type[]> duplicatedRepositories = response
                    .GroupBy(r => GetEntity(r))
                    .Where(v => v.Count() > 1)
                    .ToDictionary(k => k.Key, v => v.ToArray());
                  

                if (duplicatedRepositories.Any())
                    throw new Exception(GenerateDuplicatedMessage(duplicatedRepositories));
            }

            foreach (Type type in response)
            {
                if (AddRepository(type))
                    throw new Exception();
            }
        }

        private string GenerateDuplicatedMessage(Dictionary<Type, Type[]> duplicatedRepositories)
        {
            StringBuilder message = new StringBuilder();
            foreach(var type in duplicatedRepositories)
            {
                Type entityType = type.Key;
                Type[] repositoriesForType = type.Value;
                string repositories = string.Join(", ", repositoriesForType.Select(r => r.Name));
                message.Append($"There are [{repositories}] to [{type.Key.Name}] type.\n");
            }
            return message.ToString();
        }



        /// <returns>returns if some entity repository was replaced</returns>
        public bool AddRepository<RepositoryType>()
            where RepositoryType : IBaseRepository
        {
            Type type = typeof(RepositoryType);
            return AddRepository(type);
        }

        /// <returns>returns if some entity repository was replaced</returns>
        public bool AddRepository(Type type)
        {
            Type entity = GetEntity(type);
            if (repositories.ContainsKey(entity))
            {
                repositories[entity] = type;
                return true;
            }
            repositories.Add(entity, type);
            return false;
        }

        private Type GetEntity(Type repostoryType)
        {
            return repostoryType.BaseType.GenericTypeArguments[0];
        }
    }
}
