using Microsoft.Extensions.Configuration;
using SuperORM.ConsoleTests.UseCases;
using SuperORM.Core.Domain.Service.Repository;
using SuperORM.Core.Domain.Service.Settings;
using SuperORM.Core.Interface.Repository;
using SuperORM.TestsResource.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SuperORM.ConsoleTests
{
    class Program
    {
        private static IConfigurationRoot configuration;
        static void Main(string[] args)
        {
            configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                            .AddJsonFile("appsettings.json", false)
                            .Build();

            string mySqlConnectionString = configuration["MySqlConnection"];
            MySql.ConnectionProvider connectionProviderMySql = new MySql.ConnectionProvider(mySqlConnectionString);

            string sqlServerConnectionString = configuration["SqlServerConnection"];
            SqlServer.ConnectionProvider connectionProviderSqlServer = new SqlServer.ConnectionProvider(sqlServerConnectionString);

            Setting setting = Setting.GetInstance();
            setting.SetConnection(connectionProviderMySql);

            RepositoryRegistry repositoryRegistry = setting.GetRepositoryRegistry();
            repositoryRegistry.UseAllRepositories();
            //repositoryRegistry.AddRepository<UserRepository>();
            //repositoryRegistry.AddRepository<DocumentRepository>();
            //repositoryRegistry.AddRepository<DocumentTypeRepository>();


            //ColumnAssimilationTests.RunRepository();
            //SelectableJoins.Run();
            //TransactionsTests.RunInsertUpdate(connectionProviderSqlServer);
            //TransactionsTests.RunSelectUpdate(connectionProviderMySql);
            SelectableJoins.Run();

            //RepositoryJoins.Run(connectionProviderMySql);

            //var typeBaseRepository = typeof(IBaseRepository);
            //List<Type> response = AppDomain.CurrentDomain
            //                    .GetAssemblies()
            //                    .SelectMany(a => a.GetTypes())
            //                    .Where(p => typeBaseRepository.IsAssignableFrom(p)
            //                        && !p.IsAbstract
            //                        && !p.IsInterface)
            //                    .ToList();

            //var testeInstancia = response.LastOrDefault();
            //var instaciated = (IBaseRepository)Activator.CreateInstance(testeInstancia);

        }
    }
}
