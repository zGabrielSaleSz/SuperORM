using SuperORM.Core.Domain.Service.LinqSQL;
using SuperORM.Core.Interface;
using SuperORM.Core.Test.Complement.Model;
using SuperORM.SqlServer;
using Xunit;

namespace SuperORM.Core.Test.Linq
{
    public class SelectableTests
    {
        [Fact]
        public void Shold_BuildSelect_WhenUsingSelectable()
        {
            // Arrange
            string expected = "SELECT " +
                "[users].[id], " +
                "[users].[Name], " +
                "[users].[email], " +
                "[users].[active], " +
                "[documents].[issueDate], " +
                "[documentTypes].[name] " +
                "FROM [users] " +
                "INNER JOIN [documents] ON [users].[id] = [documents].[idUser] " +
                "LEFT JOIN [documentTypes] ON [documents].[idDocumentType] = [documentTypes].[id] " +
                "WHERE ([users].[Name] LIKE '%Gabriel%')";
            ConnectionProvider connectionProvider = new ConnectionProvider("");
            IQuerySintax querySintax = new QuerySintax();
            ISelectable<User> selectable = new Selectable<User>(connectionProvider.GetNewConnection(), querySintax);
            selectable
                .Select(
                    a => a.id,
                    a => a.Name,
                    a => a.email,
                    a => a.active
                )
                .Select<Document>(
                    d => d.issueDate
                )
                .Select<DocumentType>(
                    t => t.name
                )
                .From("users")
                .InnerJoin<Document>("documents", a => a.id, b => b.idUser)
                .LeftJoin<Document, DocumentType>("documentTypes", d => d.idDocumentType, t => t.id)
                .Where(u => u.Name.Contains("Gabriel"));

            // Act
            string result = selectable.GetQuery();

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
