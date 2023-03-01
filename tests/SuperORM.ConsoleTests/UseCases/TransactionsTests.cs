using SuperORM.Core.Domain.Model.Sql;
using SuperORM.Core.Interface.Integration;
using SuperORM.TestsResource.Entities;
using SuperORM.TestsResource.Repositories;
using System;

namespace SuperORM.ConsoleTests.UseCases
{
    internal class TransactionsTests
    {
        internal static void Run(IConnectionProvider connectionProvider)
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
