﻿using SuperORM.Core.Domain.Model.Enum;
using SuperORM.Core.Domain.Model.QueryBuilder;
using SuperORM.Core.Domain.Model.QueryBuilder.Fields;
using SuperORM.Core.Domain.Model.QueryBuilder.Fields.FieldsArgument;
using SuperORM.Core.Domain.Service.QueryBuilder;
using SuperORM.Core.Interface.QueryBuilder;
using SuperORM.Core.Test.Pattern;
using SuperORM.SqlServer;
using Xunit;

namespace SuperORM.Core.Test.QueryBuilder.Selectable
{
    public class SelectableBuilderSqlServerTests : ISelectableBuilderTest
    {
        private static SelectableBuilder GetSelectable()
        {
            return new SelectableBuilder(new QuerySintax());
        }

        [Fact]
        public void Should_BuildQuery_When_SelectableUsingString()
        {
            // Arrange
            string expected = "SELECT [users].[id], [users].[nome] FROM [users]";
            SelectableBuilder selectable = new SelectableBuilder(new QuerySintax());

            // Act
            string actual = selectable.Select("id", "nome").From("users").GetQuery();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Should_SpecifyTable_When_UsingQueryBuilder()
        {
            // Arrange
            string expected = "SELECT [users].[id] AS [ID] FROM [users]";
            SelectableBuilder selectable = new SelectableBuilder(new QuerySintax());
            Table table = new Table("users");
            IField fieldId = table.AddField<Column>("id", "ID");

            // Act
            string actual = selectable.Select(fieldId).From(table).GetQuery();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Should_BuildQuerySpecifingTabeInSelect_When_UsingAliasInTableQueryBuilder()
        {
            // Arrange
            string expected = "SELECT [Usr].[id] AS [ID] FROM [users] [Usr]";
            SelectableBuilder selectable = new SelectableBuilder(new QuerySintax());
            Table table = new Table("users", "Usr");
            IField fieldId = table.AddField<Column>("id", "ID");

            // Act
            string actual = selectable.Select(fieldId).From(table).GetQuery();

            // Assert
            Assert.Equal(expected, actual);
        }

        // Not Interface

        [Fact]
        public void Should_BuildSchemaReference_When_SelectableUsingSchema()
        {
            // Arrange
            string expected = "SELECT [dbo].[users].[id], [dbo].[users].[nome] FROM [dbo].[users]";
            string schema = "dbo";
            SelectableBuilder selectable = new SelectableBuilder(new QuerySintax());
            Table table = new Table(schema, "users", "");

            // Act
            string actual = selectable.Select("id", "nome").From(table).GetQuery();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Should_BuildQuery_When_SelectableUsingFieldsBuilder()
        {
            // Arrange
            string expected = "SELECT MIN([users].[id]), MAX([users].[id]) AS [foo] FROM [users]";
            ISelectableBuilder selectable = GetSelectable();
            Table table = new Table("users");
            IFieldsBuilder fieldsBuilder = table.GetFieldsBuilder()
                .AddField<Min>("id")
                .AddField<Max>("id", "foo");

            // Act
            string actual = selectable.Select(fieldsBuilder).From(table).GetQuery();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Should_BuildQuery_When_SelectableUsingJoin()
        {
            // Arrange
            string expected = "SELECT [users].[id] AS [IDUser], [phones].[ddi] AS [DDI] FROM [users] LEFT JOIN [phones] ON [users].[id] = [phones].[id]";
            ISelectableBuilder selectable = GetSelectable();
            Table tableUsers = new Table("users");
            IFieldsBuilder fieldsBuilderUsers = tableUsers.GetFieldsBuilder()
                .AddField("id", "IDUser");

            Table tablePhones = new Table("phones");
            IFieldsBuilder fieldsBuilderPhones = tablePhones.GetFieldsBuilder()
                .AddField("id")
                .AddField("ddi", "DDI");
            IField userIdField = fieldsBuilderUsers.Find("id");
            IField phoneIdField = fieldsBuilderPhones.Find("id");

            // Act
            string actual = selectable
                .Select(fieldsBuilderUsers.Find("id"), fieldsBuilderPhones.Find("ddi"))
                .From(tableUsers)
                .LeftJoin(userIdField, phoneIdField)
                .GetQuery();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Should_BuildQuery_When_SelectableUsingWhere()
        {
            // Arrange
            string expected = "SELECT [users].[id] AS [IDUser], [phones].[ddi] AS [DDI] FROM [users] LEFT JOIN [phones] ON [users].[id] = [phones].[id] WHERE ([users].[id] = 2)";
            SelectableBuilder selectable = GetSelectable();
            Table tableUsers = new Table("users");
            IFieldsBuilder fieldsBuilderUsers = tableUsers.GetFieldsBuilder()
                .AddField("id", "IDUser");

            Table tablePhones = new Table("phones");
            IFieldsBuilder fieldsBuilderPhones = tablePhones.GetFieldsBuilder()
                .AddField("id")
                .AddField("ddi", "DDI");
            IField userIdField = fieldsBuilderUsers.Find("id");
            IField phoneIdField = fieldsBuilderPhones.Find("id");

            // Act
            string actual = selectable.Select(fieldsBuilderUsers.Find("id"), fieldsBuilderPhones.Find("ddi"))
                .From(tableUsers)
                .LeftJoin(userIdField, phoneIdField)
                .Where(userIdField, 2)
                .GetQuery();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Should_BuildQuery_When_SelectableUsingGroupBy()
        {
            // Arrange
            string expected = "SELECT [users].[type], MAX([users].[name]) FROM [users] GROUP BY [users].[type]";
            SelectableBuilder selectable = GetSelectable();
            Table table = new Table("users");
            IFieldsBuilder fieldsBuilderPhones = table.GetFieldsBuilder()
              .AddField("type")
              .AddField<Max>("name");

            // Act
            string actual = selectable.Select(fieldsBuilderPhones)
                .From(table)
                .GroupBy(fieldsBuilderPhones.Find("type"))
                .GetQuery();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Should_BuildQuery_When_SelectableUsingLimit()
        {
            // Arrange
            string expected = "SELECT TOP(10) [users].[id], [users].[name] FROM [users]";
            SelectableBuilder selectable = GetSelectable();
            Table table = new Table("users");
            IFieldsBuilder fieldsBuilder = table.GetFieldsBuilder()
                .AddField("id")
                .AddField("name");

            // Act
            string actual = selectable.Select(fieldsBuilder)
                .From(table)
                .Limit(10)
                .GetQuery();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Should_BuildQuery_When_SelectableUsingOrderBy()
        {
            // Arrange
            string expected = "SELECT [users].[id], [users].[name], [users].[birthday] FROM [users] ORDER BY [users].[birthday] DESC, [users].[name] ASC";
            SelectableBuilder selectable = GetSelectable();
            Table table = new Table("users");
            IFieldsBuilder fieldsBuilder = table.GetFieldsBuilder()
                .AddField("id")
                .AddField("name")
                .AddField("birthday");

            // Act
            string actual = selectable.Select(fieldsBuilder)
                .From(table)
                .OrderByDescending(fieldsBuilder.Find("birthday"))
                .OrderBy(fieldsBuilder.Find("name"))
                .GetQuery();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Should_BuildQuery_When_SelectableUsingHaving()
        {
            // Arrange
            string expected = "SELECT COUNT([users].[id]), [users].[country] " +
                "FROM [users] " +
                "GROUP BY [users].[country] " +
                "HAVING (COUNT([users].[id]) > 5) " +
                "ORDER BY COUNT([users].[id]) DESC";

            SelectableBuilder selectable = GetSelectable();
            Table table = new Table("users");
            IFieldsBuilder fieldsBuilder = table.GetFieldsBuilder()
                .AddField<Count>("id")
                .AddField("country");

            IField fieldId = fieldsBuilder.Find("id");

            // Act
            string actual = selectable.Select(fieldsBuilder)
                .From(table)
                .GroupBy(fieldsBuilder.Find("country"))
                .Having(fieldId, SqlComparator.GreaterThan, 5)
                .OrderByDescending(fieldId)
                .GetQuery();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
