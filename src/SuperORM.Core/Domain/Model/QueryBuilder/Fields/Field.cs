using SuperORM.Core.Interface;
using System.Text;

namespace SuperORM.Core.Domain.Model.QueryBuilder.Fields
{
    public class Field<T> : IField
        where T : IFieldArgument, new()
    {
        private string _alias;
        private Table _table;
        private string _fieldName;

        internal Field(Table table, string fieldName, string fieldAlias = null)
        {
            _fieldName = fieldName;
            _alias = fieldAlias;
            _table = table;
        }

        public string GetFieldValue(IQuerySintax querySintax)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(_table.GetIdentifier(querySintax));
            builder.Append('.');
            builder.Append(querySintax.GetColumnName(_fieldName));
            return new T().Handle(builder.ToString());
        }

        public string GetRaw(IQuerySintax querySintax)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(GetFieldValue(querySintax));
            if (_alias != null)
                builder.Append(querySintax.GetColumnAlias(_alias));
            return builder.ToString();
        }

        public void SetTable(Table table)
        {
            _table = table;
        }

        public Table GetTable()
        {
            return _table;
        }

        public string GetFieldName()
        {
            return _fieldName;
        }
    }
}
