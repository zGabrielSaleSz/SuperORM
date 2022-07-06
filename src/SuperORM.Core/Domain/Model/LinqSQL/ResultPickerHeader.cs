using SuperORM.Core.Domain.Service.LinqSQL.SelectableTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperORM.Core.Domain.Model.LinqSQL
{
    public class ResultPickerHeader
    {
        public ColumnAssimilator ColumnAssimilator { get; private set; }
        public Dictionary<Type, Dictionary<string, object>> ValuesByType { get; private set; }
        public ResultPickerHeader(ColumnAssimilator columnAssimilator)
        {
            ValuesByType = new Dictionary<Type, Dictionary<string, object>>();
            ColumnAssimilator = columnAssimilator;
        }

        internal void Add(Type type, string fieldName, object value)
        {
            if (!ValuesByType.ContainsKey(type))
                ValuesByType.Add(type, new());
            ValuesByType[type].Add(fieldName, value);
        }
    }
}
