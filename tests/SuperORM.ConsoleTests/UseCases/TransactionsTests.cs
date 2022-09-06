using SuperORM.ConsoleTests.Repositories;
using SuperORM.Core.Domain.Model.Sql;
using SuperORM.Core.Interface;
using SuperORM.Core.Test.Complement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperORM.ConsoleTests.UseCases
{
    internal class TransactionsTests
    {
        internal static void RunInsertUpdate(IConnectionProvider connectionProvider)
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
                newUser.height = 1.7001f;
                userRepository.Insert(newUser);

                newUser.active = false;
                newUser.approvedDate = DateTime.Now.AddDays(-1);
                userRepository.Update(newUser);

                transaction.Commit();
            }
        }

        internal static void RunSelectUpdate(IConnectionProvider connectionProvider)
        {
            using (SuperTransaction transaction = new SuperTransaction(connectionProvider))
            {
                transaction.BeginTransaction();
                UserRepository userRepository = transaction.Use<UserRepository>();

                User existentUser = userRepository.GetSelectable().Where(u => u.id == 1).FirstOrDefault();
                existentUser.height = 1.91;
            
                userRepository.Update(existentUser);
                transaction.Commit();
            }
        }
    }
}
