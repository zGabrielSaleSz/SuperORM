using SuperORM.Core.Domain.Model.QueryBuilder;
using System;
using System.Collections.Generic;

namespace SuperORM.Core.Domain.Service.LinqSQL.SelectableTools
{
    public interface ITableAssimilator
    {
        Table AddTableReference(Type type, string tableName);
        Table GetMainTable();
        Table GetTableReference(Type type);
        Dictionary<Type, Table> GetTablesReference();
        Type GetTypeByTable(Table table);
        bool MainTableHasName();
        void SetMainTableName(string tableName);
        void SetMainTableName(Table table);
        void SetTableName(Type type, string tableName);
        bool TableHasReferences(Type type);
    }
}