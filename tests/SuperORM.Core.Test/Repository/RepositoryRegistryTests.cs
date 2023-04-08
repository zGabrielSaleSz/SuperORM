using SuperORM.Core.Domain.Exceptions;
using SuperORM.Core.Domain.Service.RepositoryUtils;
using SuperORM.Core.Interface.Repository;
using SuperORM.Core.Test.Complement.Mock;
using SuperORM.TestsResource.Entities;
using SuperORM.TestsResource.Repositories;
using System;
using Xunit;

namespace SuperORM.Core.Test.Repository
{
    public class RepositoryRegistryTests
    {

        private RepositoryRegistry GetNewRepositoryRegistry()
        {
            return new RepositoryRegistry(new ConnectionProviderMock());
        }

        [Fact]
        public void Should_ReturnNewInstance_When_GetConfiguredRepository()
        {
            // Arrange
            RepositoryRegistry repositoryRegistry = GetNewRepositoryRegistry();

            // Act
            repositoryRegistry.AddRepository<UserRepository>();
            IBaseRepository baseRepository = repositoryRegistry.GetRepositoryOf<User>();

            // Assert
            Assert.IsType<UserRepository>(baseRepository);
        }

        [Fact]
        public void Should_ThrowException_When_NotPreviouslyConfigured()
        {
            // Arrange
            RepositoryRegistry repositoryRegistry = GetNewRepositoryRegistry();

            // Act
            Action action = () =>
            {
                IBaseRepository baseRepository = repositoryRegistry.GetRepositoryOf<User>();
            };

            // Assert
            Assert.Throws<EntityNotConfiguredException>(action);
        }

        [Fact]
        public void Should_ReplaceRepository_When_SameRepositoryTargetIsAdded()
        {
            // Arrange
            RepositoryRegistry repositoryRegistry = GetNewRepositoryRegistry();

            // Act
            repositoryRegistry.AddRepository<UserRepository>();
            repositoryRegistry.AddRepository<UserRepositoryNewImplementation>();

            IBaseRepository baseRepository = repositoryRegistry.GetRepositoryOf<User>();

            // Assert
            Assert.IsAssignableFrom<UserRepositoryNewImplementation>(baseRepository);
        }

        [Fact]
        public void Should_ThrowException_When_UsingAllRepositoriesIsNotIgnoringDuplicates()
        {
            // Arrange
            RepositoryRegistry repositoryRegistry = GetNewRepositoryRegistry();

            // Act
            Action action = () =>
            {
                repositoryRegistry.UseAllRepositories(
                    ignoreDuplicate: false,
                    assemblies: AppDomain.CurrentDomain.GetAssemblies()
                );
            };

            // Assert
            Assert.Throws<DuplicatedRepositoryImplementationException>(action);
        }

        [Fact]
        public void Should_AddAllRepositoriesImplementations_When_UsingAllRepositoriesIgnoringDuplicates()
        {
            // Arrange
            RepositoryRegistry repositoryRegistry = GetNewRepositoryRegistry();

            // Act
            //[add all except the duplicates]
            repositoryRegistry.UseAllRepositories(
                ignoreDuplicate: true,
                assemblies: AppDomain.CurrentDomain.GetAssemblies()
            );
            //[add the one I want from duplicates]
            repositoryRegistry.AddRepository<UserRepository>();

            // Assert
            Assert.NotNull(repositoryRegistry.GetRepositoryOf<Document>());
            Assert.NotNull(repositoryRegistry.GetRepositoryOf<DocumentType>());
            Assert.NotNull(repositoryRegistry.GetRepositoryOf<NullTest>());
            Assert.NotNull(repositoryRegistry.GetRepositoryOf<OldUser>());
            Assert.NotNull(repositoryRegistry.GetRepositoryOf<User>());
        }
    }
}
