using SuperORM.Core.Domain.Exceptions;
using SuperORM.Core.Interface;
using SuperORM.Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace SuperORM.Core.Domain.Service.Repository
{
    public class RepositoryRegistry
    {
        private IConnectionProvider _connectionProvider;
        private Dictionary<Type, Type> _repositories;
        public RepositoryRegistry(IConnectionProvider connectionProvider)
        {
            if (connectionProvider == null)
                throw new MissingConnectionProviderException("Please set the ConnectionProvider!");

            _connectionProvider = connectionProvider;
            _repositories = new Dictionary<Type, Type>();
        }

        public IBaseRepository GetRepository<T>()
        {
            return GetRepository(typeof(T));
        }

        public IBaseRepository GetRepository(Type type)
        {
            if (!_repositories.ContainsKey(type))
                throw new EntityNotConfiguredException($"Repository could not be found for '{type.AssemblyQualifiedName}'");
            return (IBaseRepository)Activator.CreateInstance(_repositories[type], _connectionProvider);
        }

        public void UseAllRepositories(bool ignoreDuplicate, params Assembly[] assemblies)
        {
            List<Type> response = GetAllRepositoriesImplementation(assemblies);
            if (ignoreDuplicate)
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
                    throw new DuplicatedRepositoryImplementation(GenerateDuplicatedMessage(duplicatedRepositories));
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

        private List<Type> GetAllRepositoriesImplementation(params Assembly[] assemblies)
        {
            var typeBaseRepository = typeof(IBaseRepository);
            return
                assemblies
                .SelectMany(a => a.GetTypes())
                .Where(p => typeBaseRepository.IsAssignableFrom(p)
                    && !p.IsAbstract
                    && !p.IsInterface)
                .ToList();
        }

        /// <returns>if some entity repository was replaced</returns>
        public bool AddRepository<RepositoryType>()
            where RepositoryType : IBaseRepository
        {
            Type type = typeof(RepositoryType);
            return AddRepository(type);
        }

        /// <returns>if some entity repository was replaced</returns>
        public bool AddRepository(Type type)
        {
            Type entity = GetEntity(type);
            if (_repositories.ContainsKey(entity))
            {
                _repositories[entity] = type;
                return true;
            }
            _repositories.Add(entity, type);
            return false;
        }

        private Type GetEntity(Type repostoryType)
        {
            return repostoryType.BaseType.GenericTypeArguments[0];
        }
    }
}
