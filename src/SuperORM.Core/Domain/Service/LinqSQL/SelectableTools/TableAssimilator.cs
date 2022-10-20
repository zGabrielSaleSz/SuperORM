using SuperORM.Core.Domain.Exceptions;
using SuperORM.Core.Domain.Model.QueryBuilder;
using System;
using System.Collections.Generic;
using System.Linq;

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
            mainTable.SetName(tableName);
        }

        internal void SetMainTableName(Table table)
        {
            mainTable.CopyFrom(table);
        }

        internal Table GetMainTable()
        {
            return mainTable;
        }

        internal Dictionary<Type, Table> GetTablesReference()
        {
            return tables;
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
            tables[type].SetName(tableName);
        }

        internal Table GetTableReference(Type type)
        {
            CheckTableReference(type);
            return tables[type];
        }

        internal Type GetTypeByTable(Table table)
        {
            return tables.Where(t => t.Value == table).Select(t => t.Key).FirstOrDefault();
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
