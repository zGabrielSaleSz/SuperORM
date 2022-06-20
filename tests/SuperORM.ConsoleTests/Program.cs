using Microsoft.Extensions.Configuration;
using SuperORM.ConsoleTests.Repositories;
using SuperORM.Core.Domain.Model.Sql;
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
            TesteTransaction(connectionProvider);

            UserRepository userRepository = new UserRepository();

            int[] ids = new int[] { 1, 2 };

            string[] names = new string[] { "Gabriel again", "gabriel2" };

            ISelectable<User> selectable = userRepository.Select()
                .Where(u => u.id == 1 /*&& (u.Name == "Gabriel" || names.Contains(u.Name)) && ids.Contains(u.id) && u.email.Contains("gabriel") && u.active == false*/)
                .OrderByDescending(u => u.id);

            string query = selectable.GetQuery();
            User[] user = selectable
                .AsEnumerable()
                .ToArray();

            User specialUser = user.First();

            specialUser.Name = "Gabriel again";
            userRepository.Update(specialUser);

            //userRepository.Delete(new User { id = 9});
            Console.WriteLine("Hello World!");
        }

        private static void TesteTransaction(ConnectionProvider connectionProvider)
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

    class TesteRetorno
    {
        public int ID { get; set; }
        public bool Ativo { get; set; }
    }
}
