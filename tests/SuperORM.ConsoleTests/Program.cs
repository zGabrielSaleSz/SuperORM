using Microsoft.Extensions.Configuration;
using SuperORM.ConsoleTests.Repositories;
using SuperORM.Core.Domain.Model.LinqSQL;
using SuperORM.Core.Domain.Model.Sql;
using SuperORM.Core.Domain.Service.LinqSQL.SelectableTools;
using SuperORM.Core.Domain.Service.Settings;
using SuperORM.Core.Interface;
using SuperORM.Core.Test.Complement.Model;
using SuperORM.MySql;
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
            ConnectionProvider connectionProvider = new MySql.ConnectionProvider(mySqlConnectionString);

            Setting setting = Setting.GetInstance();
            setting.SetConnection(connectionProvider);

            UserRepository userRepository = new UserRepository();

            int[] ids = new int[] { 1, 2 };

            string[] names = new string[] { "Gabriel again", "gabriel goto" };

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
           
            var handledResult = result.Select(r => new
            {
                user = r.From<User>(),
                document = r.From<Document>()
            });

            string query = selectable.GetQuery();
            User[] user = selectable
                .AsEnumerable()
                .ToArray();

            User specialUser = user.First();
            specialUser.Name = "Gabriel again";
            userRepository.Update(specialUser);

            TransactionTest(connectionProvider);
        }

        private static void TransactionTest(ConnectionProvider connectionProvider)
        {
            using (SuperTransaction transaction = new SuperTransaction(connectionProvider))
            {
                transaction.BeginTransaction();
                UserRepository userRepository = transaction.Use<UserRepository>();

                User newUser = new User();
                newUser.Name = "Transaction";
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
