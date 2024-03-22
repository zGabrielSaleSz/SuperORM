using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
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
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
            {
                Server = "ec2-user@ec2-54-233-188-132.sa-east-1.compute.amazonaws.com",
                Port = 3306,
                UserID = "root",
                Password = "esvaed16",
                Database = "zdatabase",
                SslMode = MySqlSslMode.Required, // Define o modo SSL como obrigatório
                CertificateFile = @"D:\Users\Gabriel\Desktop\aws\ssh_sql.pem" // Especifique o caminho para o certificado PEM
            };



            MySql.ConnectionProvider connectionProviderMySql = new MySql.ConnectionProvider(builder.ToString());

            //string sqlServerConnectionString = configuration["SqlServerConnection"];
            //SqlServer.ConnectionProvider connectionProviderSqlServer = new SqlServer.ConnectionProvider(sqlServerConnectionString);

            Setting setting = Setting.GetInstance();
            setting.SetConnection(connectionProviderMySql);

            ColumnAssimilationTests.RunRepository();
            SelectableJoins.Run();
            TransactionsTests.Run(connectionProviderMySql);
        }
    }
}
