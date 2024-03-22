using SuperORM.Core.Domain.Exceptions;
using SuperORM.Core.Domain.Model.QueryBuilder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperORM.Core.Domain.Service.LinqSQL.SelectableTools
{
    internal class TableAssimilator : ITableAssimilator
    {
        private readonly Table mainTable;
        private readonly Dictionary<Type, Table> tables = new Dictionary<Type, Table>();
        internal TableAssimilator(Type typeMainTable)
        {
            mainTable = new Table("");
            tables.Add(typeMainTable, mainTable);
        }

        public void SetMainTableName(string tableName)
        {
            mainTable.Name = tableName;
        }

        public void SetMainTableName(Table table)
        {
            mainTable.CopyFrom(table);
        }

        public Table GetMainTable()
        {
            return mainTable;
        }

        public Dictionary<Type, Table> GetTablesReference()
        {
            return tables;
        }

        public bool MainTableHasName()
        {
            return !string.IsNullOrWhiteSpace(mainTable.Name);
        }

        public Table AddTableReference(Type type, string tableName)
        {
            if (TableHasReferences(type))
                throw new TableReferenceException("This type already have a table referenced");

            tables.Add(type, new Table(tableName));
            return tables[type];
        }

        public void SetTableName(Type type, string tableName)
        {
            CheckTableReference(type);
            tables[type].Name = tableName;
        }

        public Table GetTableReference(Type type)
        {
            CheckTableReference(type);
            return tables[type];
        }

        public Type GetTypeByTable(Table table)
        {
            return tables.Where(t => t.Value == table).Select(t => t.Key).FirstOrDefault();
        }

        public bool TableHasReferences(Type type)
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
