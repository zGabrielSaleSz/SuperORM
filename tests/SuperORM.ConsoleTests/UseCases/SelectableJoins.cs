using SuperORM.Core.Domain.Model.LinqSQL;
using SuperORM.Core.Domain.Service.LinqSQL;
using SuperORM.Core.Domain.Service.Settings;
using SuperORM.Core.Interface;
using SuperORM.TestsResource.Entities;
using SuperORM.TestsResource.Repositories;
using System.Linq;

namespace SuperORM.ConsoleTests.UseCases
{
    internal class SelectableJoins
    {
        internal static void Run()
        {
            IConnectionProvider connectionProvider = Setting.GetInstance().ConnectionProvider;
            UserRepository userRepository = new UserRepository(connectionProvider);

            ISelectable<User> selectable = new Selectable<User>(connectionProvider.GetNewConnection(), connectionProvider.GetQuerySintax())
                //userRepository.GetSelectable()
                .Select<User>(
                    u => u.id,
                    u => u.Name
                )
                .Select<Document>(
                    d => d.id,
                    d => d.number
                )
                .Select<DocumentType>(
                    dt => dt.description
                )
                .From<User>()
                .InnerJoin<Document>(a => a.id, d => d.idUser)
                .InnerJoin<Document, DocumentType>(a => a.idDocumentType, dt => dt.id)
                .Where(u => u.id == 1)
                .OrderByDescending(u => u.id);

            string queryResult = selectable.GetQuery();
            ResultPicker[] result = selectable.GetResult().ToArray();

            // retrieve results from your joins
            var handledResult = result.Select(r => new
            {
                user = r.From<User>(),
                document = r.From<Document>(),
                documentName = r.From<DocumentType>().description
            }).ToArray();
        }
    }
}
