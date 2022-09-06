using Microsoft.Extensions.Configuration;
using SuperORM.ConsoleTests.Repositories;
using SuperORM.ConsoleTests.UseCases;
using SuperORM.Core.Domain.Model.LinqSQL;
using SuperORM.Core.Domain.Model.Sql;
using SuperORM.Core.Domain.Service.LinqSQL;
using SuperORM.Core.Domain.Service.LinqSQL.SelectableTools;
using SuperORM.Core.Domain.Service.Settings;
using SuperORM.Core.Interface;
using SuperORM.Core.Interface.LinqSQL;
using SuperORM.Core.Test.Complement.Model;
using System;
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

            //ColumnAssimilationTests.RunRepository();
            //SelectableJoins.Run();
            //TransactionsTests.RunInsertUpdate(connectionProviderSqlServer);
            //TransactionsTests.RunSelectUpdate(connectionProviderMySql);
            RepositoryJoins.Run(connectionProviderMySql);
        }
    }
}
