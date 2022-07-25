using SuperORM.ConsoleTests.Repositories;
using SuperORM.Core.Domain.Model.LinqSQL;
using SuperORM.Core.Interface;
using SuperORM.Core.Test.Complement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperORM.ConsoleTests.UseCases
{
    internal class SelectableJoins
    {
        internal static void Run()
        {
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

            // retrieve results from your joins
            var handledResult = result.Select(r => new
            {
                user = r.From<User>(),
                document = r.From<Document>()
            }).ToArray();
        }
    }
}
