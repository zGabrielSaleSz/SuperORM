using Microsoft.Extensions.Configuration;
using SuperORM.ConsoleTests.Repositories;
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

            ColumnAssimilationTest(connectionProviderMySql);
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

        private static void ColumnAssimilationTest(IConnectionProvider connectionProvider)
        {
            IConnection connection = connectionProvider.GetNewConnection();
            IQuerySintax querySintax = connectionProvider.GetQuerySintax();

            ColumnAssimilator columnAssimilator = new ColumnAssimilator();
            columnAssimilator.Add<OldUser>("ID", "id");
            columnAssimilator.Add<OldUser>("Name", "strName");
            columnAssimilator.Add<OldUser>("Email", "strEmail");
            columnAssimilator.Add<OldUser>("Password", "strPassword");
            columnAssimilator.Add<OldUser>("Active", "blnActive");
            columnAssimilator.Add<OldUser>("ApprovedDate", "approvedDt");

            var selectable = new Selectable<OldUser>(connection, querySintax);
            var query = selectable
                .AddColumnAssimilation(columnAssimilator)
                .SelectAll()
                .From("oldUsers")
                .Where(u => u.ID == 1 || u.Name.Contains("gabriel") || u.Active == true)
                .OrderBy(u => u.ID).OrderByDescending(u => u.Name);
            string queryResult = query.GetQuery();
            OldUser result = query.FirstOrDefault();


            var insertable = new Insertable<OldUser>(connection, querySintax);
            OldUser values = new OldUser
            {
                Name = "Super Orm",
                Email = "gabriel.s479@hotmail.com",
                Active = true,
                Password = "SUPERAw3s0meP4ssw0rd",
                ApprovedDate = DateTime.Now,
            };
            IInsertable<OldUser> insertQuery = insertable
                .AddColumnAssimilation(columnAssimilator)
                .Into("oldUsers")
                .Ignore("ID")
                .Values(values);

            string insertQueryResult = insertQuery.GetQuery();
            int insertedRowsChanged = insertQuery.Execute();

            var updatable = new Updatable<OldUser>(connection, querySintax);
            IUpdatable<OldUser> updatableQuery = updatable
                .AddColumnAssimilation(columnAssimilator)
                .Update("oldUsers")
                .Set(a => a.Active, false)
                .Where(u => u.Email == "gabriel.s479@hotmail.com");
            string updatableQueryResult = updatable.GetQuery();
            int updatableRowsChanged = updatable.Execute();

            var deletable = new Deletable<OldUser>(connection, querySintax);
            IDeletable<OldUser> deletableQuery = deletable
                .AddColumnAssimilation(columnAssimilator)
                .From("oldUsers")
                .Where(a => a.ID < 11);

            string deletableQueryResult = deletable.GetQuery();
            int deletableRowsChanged = deletable.Execute();
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
