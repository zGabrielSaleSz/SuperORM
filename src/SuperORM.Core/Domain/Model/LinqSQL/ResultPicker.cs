using SuperORM.Core.Domain.Service.LinqSQL.SelectableTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SuperORM.Core.Domain.Model.LinqSQL
{
    public class ResultPicker 
    {
        private readonly ResultPickerHeader _resultPickerHeader;

        internal ResultPicker(ResultPickerHeader resultPickerHeader)
        {
            _resultPickerHeader = resultPickerHeader;
        }

        public T From<T>() where T : new()
        {
            if (!_resultPickerHeader.ValuesByType.ContainsKey(typeof(T)))
                throw new ArgumentException("Specified object type is not into your result data");

            Dictionary<string, object> keyValueObject = _resultPickerHeader.ValuesByType[typeof(T)];
            return EntityBuilder.Build<T>(keyValueObject, _resultPickerHeader.ColumnAssimilator);
        }
    }
}
