using SuperORM.Core.Domain.Exceptions;
using SuperORM.Core.Domain.Model.QueryBuilder;
using System;
using System.Collections.Generic;

namespace SuperORM.Core.Domain.Service.LinqSQL.SelectableTools
{
    internal class TableAssimilator
    {
        private readonly Table mainTable;
        private readonly Dictionary<Type, Table> tables = new Dictionary<Type, Table>();
        internal TableAssimilator(Type typeMainTable)
        {
            mainTable = new Table("");
            tables.Add(typeMainTable, mainTable);
        }

        internal void SetMainTableName(string tableName)
        {
            mainTable.Name = tableName;
        }

        internal void SetMainTableName(Table table)
        {
            mainTable.CopyFrom(table);
        }

        internal Table GetMainTable()
        {
            return mainTable;
        }

        internal bool MainTableHasName()
        {
            return !string.IsNullOrWhiteSpace(mainTable.Name);
        }

        internal Table AddTableReference(Type type, string tableName)
        {
            if (TableHasReferences(type))
                throw new TableReferenceException("This type already have a table referenced");

            tables.Add(type, new Table(tableName));
            return tables[type];
        }

        internal void SetTableName(Type type, string tableName)
        {
            CheckTableReference(type);
            tables[type].Name = tableName;
        }

        internal Table GetTableReference(Type type)
        {
            CheckTableReference(type);
            return tables[type];
        }

        internal bool TableHasReferences(Type type)
        {
            return tables.ContainsKey(type);
        }

        private void CheckTableReference(Type type)
        {
            if (!TableHasReferences(type))
                throw new TableReferenceException("This table are not referenced");
        }
    }
}
