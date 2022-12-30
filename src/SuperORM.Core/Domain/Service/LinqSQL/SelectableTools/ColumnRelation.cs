using SuperORM.Core.Utilities;
using System;

namespace SuperORM.Core.Domain.Service.LinqSQL.SelectableTools
{
    internal class ColumnRelation
    {
        internal Type Type { get; set; }
        private readonly TwoWayKeyDicionary<string> _columnsEquivalent;
        internal ColumnRelation(Type type)
        {
            Type = type;
            _columnsEquivalent = new TwoWayKeyDicionary<string>();
        }

        internal void Add(string column, string respectiveColumn)
        {
            _columnsEquivalent.Add(column, respectiveColumn);
        }

        internal string GetByProperty(string property)
        {
            return _columnsEquivalent.GetValueFromLeftKey(property);
        }

        internal string GetByColumnValue(string column)
        {
            return _columnsEquivalent.GetValueFromRightAsKey(column);
        }
    }
}
