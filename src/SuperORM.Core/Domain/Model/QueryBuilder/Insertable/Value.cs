using System.Collections.Generic;
using System.Linq;

namespace SuperORM.Core.Domain.Model.QueryBuilder.Insertable
{
    public class Value
    {
        private Dictionary<string, object> _columnValues;

        public Value()
        {
            _columnValues = new Dictionary<string, object>();
        }

        public Value Add(string columnName, object columnValue)
        {
            _columnValues.Add(columnName, columnValue);
            return this;
        }

        public IEnumerable<string> GetColumnNames()
        {
            return _columnValues.Select(c => c.Key);
        }

        public object GetValue(string columnName)
        {
            return _columnValues[columnName];
        }
    }
}
