using Moq;
using SuperORM.Core.Domain.Exceptions;
using SuperORM.Core.Domain.Service.Repository;
using SuperORM.Core.Domain.Service.Settings;
using SuperORM.Core.Interface;
using SuperORM.Core.Interface.Repository;
using SuperORM.Core.Test.Complement.Mock;
using SuperORM.MySql;
using SuperORM.TestsResource.Entities;
using SuperORM.TestsResource.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SuperORM.Core.Test.Repository
{
    public class RepositoryRegistryTests
    {

        private IConnectionProvider GetIConnectionProviderMock()
        {
            return new ConnectionProviderMock();
        }
        [Fact]
        public void Should_ReturnNewInstance_When_GetConfiguredRepository()
        {
            // Arrange
            RepositoryRegistry repositoryRegistry = new RepositoryRegistry(GetIConnectionProviderMock());

            // Act
            repositoryRegistry.AddRepository<UserRepository>();
            IBaseRepository baseRepository = repositoryRegistry.GetRepository<User>();

            // Assert
            Assert.IsType<UserRepository>(baseRepository);
        }

        [Fact]
        public void Should_ThrowException_When_NotPreviouslyConfigured()
        {
            // Arrange
            RepositoryRegistry repositoryRegistry = new RepositoryRegistry(GetIConnectionProviderMock());

            // Act
            Action action = () =>
            {
                IBaseRepository baseRepository = repositoryRegistry.GetRepository<User>();
            };

            // Assert
            Assert.Throws<EntityNotConfiguredException>(action);
        }

        [Fact]
        public void Should_ReplaceRepository_When_SameRepositoryTargetIsAdded()
        {
            // Arrange
            RepositoryRegistry repositoryRegistry = new RepositoryRegistry(GetIConnectionProviderMock());

            // Act
            repositoryRegistry.AddRepository<UserRepository>();
            repositoryRegistry.AddRepository<UserRepositoryNewImplementation>();

            IBaseRepository baseRepository = repositoryRegistry.GetRepository<User>();

            // Assert
            Assert.IsAssignableFrom<UserRepositoryNewImplementation>(baseRepository);
        }

        [Fact]
        public void Should_ThrowException_When_UsingAllRepositoriesIsNotIgnoringDuplicates()
        {
            // Arrange
            RepositoryRegistry repositoryRegistry = new RepositoryRegistry(GetIConnectionProviderMock());

            // Act
            Action action = () =>
            {
                repositoryRegistry.UseAllRepositories();
            };

            // Assert
            Assert.Throws<DuplicatedRepositoryImplementation>(action);
        }

        [Fact]
        public void Should_AddAllRepositoriesImplementations_When_UsingAllRepositoriesIgnoringDuplicates()
        {
            // Arrange
            RepositoryRegistry repositoryRegistry = new RepositoryRegistry(GetIConnectionProviderMock());

            // Act

            //[add all except the duplicates]
            repositoryRegistry.UseAllRepositories(ignoreDuplicate: true);

            //[Add the one I want from duplicates]
            repositoryRegistry.AddRepository<UserRepository>();


            // Assert
            Assert.NotNull(repositoryRegistry.GetRepository<Document>());
            Assert.NotNull(repositoryRegistry.GetRepository<DocumentType>());
            Assert.NotNull(repositoryRegistry.GetRepository<NullTest>());
            Assert.NotNull(repositoryRegistry.GetRepository<OldUser>());
            Assert.NotNull(repositoryRegistry.GetRepository<User>());

        }
    }
}
