using SuperORM.Core.Domain.Model.QueryBuilder.Fields;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperORM.Core.Domain.Service.LinqSQL.SelectableTools
{
    public class MultipleFieldAssimilator
    {
        private IEnumerable<IField> _fields;
        public MultipleFieldAssimilator(IEnumerable<IField> fields)
        {
            _fields = fields.ToList();
        }

        public void UpdateUniqueAlias()
        {
            foreach (IField field in _fields)
            {
                field.SetAlias(GetFieldFullName(field));
            }
        }

        public IField GetFieldByAlias(string alias)
        {
            return _fields.Where(f => f.GetAlias() == alias).FirstOrDefault();
        }

        private string GetFieldFullName(IField field)
        {
            return $"{field.GetTable().Name}|{field.GetFieldName()}";
        }
    }
}
