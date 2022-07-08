using Microsoft.Extensions.Configuration;
using SuperORM.ConsoleTests.Repositories;
using SuperORM.Core.Domain.Model.LinqSQL;
using SuperORM.Core.Domain.Model.Sql;
using SuperORM.Core.Domain.Service.Settings;
using SuperORM.Core.Interface;
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
            setting.SetConnection(connectionProviderSqlServer);

            NullTestsRepository nullTestsRepository = new NullTestsRepository();
            var resultWithNull = nullTestsRepository.Select().SelectAll().AsEnumerable().ToArray();

            UserRepository userRepository = new UserRepository();

            ISelectable<User> selectable =
                userRepository.Select()
                .Select<User>(
                    u => u.id,
                    u => u.Name
                )
                .Select<Document>(
                    d => d.id,
                    d => d.number
                )
                .InnerJoin<Document>("documents", a => a.id, d => d.idUser)
                .Where(u => u.id == 1)
                .OrderByDescending(u => u.id);

            string queryResult = selectable.GetQuery();
            ResultPicker[] result = selectable.GetResult().ToArray();

            // retrie results from your joins
            var handledResult = result.Select(r => new
            {
                user = r.From<User>(),
                document = r.From<Document>()
            }).ToArray();

            TransactionTest(connectionProviderSqlServer);
        }

        private static void TransactionTest(IConnectionProvider connectionProvider)
        {
            using (SuperTransaction transaction = new SuperTransaction(connectionProvider))
            {
                transaction.BeginTransaction();
                UserRepository userRepository = transaction.Use<UserRepository>();

                User newUser = new User();
                newUser.Name = "New Transaction";
                newUser.active = true;
                newUser.password = "NotThatSecret";
                newUser.email = "gabriel.s479@hotmail.com";
                newUser.approvedDate = DateTime.Now;
                userRepository.Insert(newUser);

                newUser.active = false;
                newUser.approvedDate = DateTime.Now.AddDays(-1);
                userRepository.Update(newUser);

                transaction.Commit();

            }
        }
    }
}
