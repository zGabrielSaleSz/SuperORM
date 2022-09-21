using SuperORM.ConsoleTests.Repositories;
using SuperORM.Core.Interface;
using SuperORM.Core.Test.Complement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperORM.ConsoleTests.UseCases
{
    public class RepositoryJoins
    {
        public static void Run(IConnectionProvider connectionProvider)
        {
            UserRepository userRepository = new UserRepository();
            userRepository.UseConnectionProvider(connectionProvider);

            DocumentRepository documentRepository = new DocumentRepository();
            documentRepository.UseConnectionProvider(connectionProvider);

            ISelectable<Document> query = documentRepository.GetSelectable();
            //var result = query.InnerJoin(userRepository, a => a.user)
            //    //.SelectAll()
            //    .Where(u => u.id == 1)
            //    .GetResult()
            //    .ToArray();

            //var finalResult = result.Select(r => new
            //{
            //    user = r.From<User>(),
            //    document = r.From<Document>()
            //}).ToArray();

            //// Teste com JOIN
            //ISelectable<Document> query = documentRepository.GetSelectable();
            //var result = query
            //    .InnerJoin(userRepository, u => u.idUser, d => d.id)
            //    .Where(u => u.id == 1)
            //    .GetResult()
            //    .ToArray();

            //var finalResult = result.Select(r => new
            //{
            //    user = r.From<User>(),
            //    document = r.From<Document>()
            //}).ToArray();

            //ISelectable<Document> query = documentRepository.GetSelectable();
            //var result = query
            //    .InnerJoin(userRepository, u => u.idUser, d => d.id)
            //    .Where(u => u.idUser == 1)
            //    .GetResult()
            //    .ToArray();

            //var finalResult = result.Select(r => new
            //{
            //    user = r.From<User>(),
            //    document = r.From<Document>()
            //}).ToArray();
        }
    }
}
