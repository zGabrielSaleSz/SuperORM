using SuperORM.Core.Domain.Service.LinqSQL;
using SuperORM.Core.Domain.Service.LinqSQL.SelectableTools;
using SuperORM.Core.Interface;
using SuperORM.SqlServer;
using SuperORM.TestsResource.Entities;
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
                "[documentTypes].[description] " +
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
                    t => t.description
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

        [Fact]
        public void Shold_BuildCustomFields_When_UsingColumnAssimilator()
        {
            // Arrange
            string expected = "SELECT " +
                "[users].[ID], " +
                "[users].[Name], " +
                "[users].[Email], " +
                "[users].[Active], " +
                "[documents].[IssueDate], " +
                "[documentTypes].[Name] " +
                "FROM [users] " +
                "INNER JOIN [documents] ON [users].[ID] = [documents].[IDUser] " +
                "LEFT JOIN [documentTypes] ON [documents].[IDDocumentType] = [documentTypes].[id] " +
                "WHERE ([users].[Name] LIKE '%Gabriel%')";

            ColumnAssimilator columnAssimilator = new();
            columnAssimilator.Add<User>("id", "ID");
            columnAssimilator.Add<User>("Name", "Name");
            columnAssimilator.Add<User>("email", "Email");
            columnAssimilator.Add<User>("issueDate", "IssueDate");
            columnAssimilator.Add<User>("active", "Active");
            columnAssimilator.Add<Document>("idUser", "IDUser");
            columnAssimilator.Add<Document>("idDocumentType", "IDDocumentType");
            columnAssimilator.Add<Document>("issueDate", "IssueDate");
            columnAssimilator.Add<DocumentType>("description", "Name");

            ConnectionProvider connectionProvider = new ConnectionProvider("");
            IQuerySintax querySintax = new QuerySintax();
            ISelectable<User> selectable = new Selectable<User>(connectionProvider.GetNewConnection(), querySintax);
            selectable
                .AddColumnAssimilation(columnAssimilator)
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
                    t => t.description
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
