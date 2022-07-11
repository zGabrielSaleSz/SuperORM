using SuperORM.Core.Utilities;
using System;
using System.Collections.Generic;

namespace SuperORM.Core.Domain.Service.LinqSQL.SelectableTools
{
    public class ColumnAssimilator<T> : ColumnAssimilator
    {
        public void Add(string column, string respectiveColumn)
        {
            Add<T>(column, respectiveColumn);
        }

        public string GetByProperty(string column)
        {
            return GetByProperty<T>(column);
        }

        public string GetByColumnValue(string columnValue)
        {
            return GetByColumnValue<T>(columnValue);
        }
    }

    public class ColumnAssimilator
    {
        private static ColumnAssimilator EmptyInstance = new ColumnAssimilator();
        private readonly Dictionary<Type, ColumnRelation> columnsEquivalent;

        public ColumnAssimilator()
        {
            this.columnsEquivalent = new Dictionary<Type, ColumnRelation>();
        }

        public static ColumnAssimilator Empty
        {
            get
            {
                return EmptyInstance;
            }
        }

        public void Add<T>(string column, string respectiveColumn)
        {
            Type type = typeof(T);
            Add(type, column, respectiveColumn);
        }

        public void Add(Type type, string column, string respectiveColumn)
        {
            if (!columnsEquivalent.ContainsKey(type))
            {
                columnsEquivalent.Add(type, new ColumnRelation(type));
            }
            columnsEquivalent[type].Add(column, respectiveColumn);
        }

        public string GetByProperty<T>(string property)
        {
            Type type = typeof(T);
            return GetByProperty(type, property);
        }

        public string GetByProperty(Type type, string property)
        {
            if (!columnsEquivalent.ContainsKey(type))
                return property;

            return columnsEquivalent[type].GetByProperty(property);
        }

        public string GetByColumnValue<T>(string columnValue)
        {
            Type type = typeof(T);
            return GetByColumnValue(type, columnValue);
        }

        public string GetByColumnValue(Type type, string columnValue)
        {
            if (!columnsEquivalent.ContainsKey(type))
                return columnValue;
            return columnsEquivalent[type].GetByColumnValue(columnValue);
        }
    }

    internal class ColumnRelation
    {
        internal Type Type { get; set; }
        private readonly TwoWayKeyDicionary<string> _columnsEquivalent;
        public ColumnRelation(Type type)
        {
            Type = type;
            _columnsEquivalent = new TwoWayKeyDicionary<string>();
        }

        public void Add(string column, string respectiveColumn)
        {
            _columnsEquivalent.Add(column, respectiveColumn);
        }

        public string GetByProperty(string property)
        {
            return _columnsEquivalent.GetValueFromLeftKey(property);
        }

        public string GetByColumnValue(string column)
        {
            return _columnsEquivalent.GetValueFromRightAsKey(column);
        }
    }
}
