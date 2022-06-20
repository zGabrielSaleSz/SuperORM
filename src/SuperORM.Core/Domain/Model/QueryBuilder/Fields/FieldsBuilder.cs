using SuperORM.Core.Domain.Model.QueryBuilder.Fields.FieldsArgument;
using System.Collections.Generic;
using System.Linq;

namespace SuperORM.Core.Domain.Model.QueryBuilder.Fields
{
    public class FieldsBuilder : IFieldsBuilder
    {
        private readonly Table Table;
        private readonly List<IField> Fields;
        internal FieldsBuilder(Table table)
        {
            this.Table = table;
            this.Fields = new List<IField>();
        }

        public IFieldsBuilder AddField<T>(string fieldName, string fieldAlias = null) where T : IFieldArgument, new()
        {
            IField field = new Field<T>(Table, fieldName, fieldAlias);
            this.Fields.Add(field);
            return this;
        }

        public IField Find(string fieldName)
        {
            return Fields.Where(f => f.GetFieldName() == fieldName).FirstOrDefault();
        }

        public IFieldsBuilder AddField(string fieldName, string fieldAlias = null)
        {
            return AddField<Column>(fieldName, fieldAlias);
        }

        public IEnumerable<IField> GetResult()
        {
            return Fields;
        }
    }
}
