using SuperORM.Core.Domain.Model.QueryBuilder.Fields;
using SuperORM.Core.Interface;
using System.Text;

namespace SuperORM.Core.Domain.Model.QueryBuilder
{
    public class Table
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Schema { get; set; }

        private IFieldsBuilder FieldsBuilder;
        public Table()
        {
            Initialize(null);
        }

        public Table(string tableName)
        {
            Initialize(tableName);
        }
        public Table(string tableName, string alias)
        {
            Initialize(tableName, null, alias);
        }
        public Table(string schema, string tableName, string alias)
        {
            Initialize(tableName, schema, alias);
        }

        public void CopyFrom(Table table)
        {
            this.Name = table.Name;
            this.Alias = table.Alias;
            this.Schema = table.Schema;
            this.FieldsBuilder = table.FieldsBuilder;
        }

        private void Initialize(string tableName, string schema = null, string alias = null)
        {
            this.Name = tableName;
            this.Alias = alias;
            this.Schema = schema;
            FieldsBuilder = new FieldsBuilder(this);
        }

        public IField AddField<T>(string fieldName, string fieldAlias = null) where T : IFieldArgument, new()
        {
            return FieldsBuilder.AddField<T>(fieldName, fieldAlias).Find(fieldName);
        }

        public IFieldsBuilder GetFieldsBuilder()
        {
            return FieldsBuilder;
        }

        internal string GetAsTableName(IQuerySintax querySintax)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(GetFullTableName(querySintax));
            if (HasAlias())
            {
                stringBuilder.Append(' ');
                stringBuilder.Append(GetAlias(querySintax));
            }
            return stringBuilder.ToString();
        }

        internal string GetFullTableName(IQuerySintax querySintax)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(Schema))
            {
                stringBuilder.Append(querySintax.GetTableSchema(Schema));
                stringBuilder.Append('.');
            }
            stringBuilder.Append(querySintax.GetTableName(Name));
            return stringBuilder.ToString();
        }

        public bool HasName()
        {
            return !string.IsNullOrWhiteSpace(Name);
        }

        internal bool HasAlias()
        {
            return !string.IsNullOrWhiteSpace(Alias);
        }

        internal string GetIdentifier(IQuerySintax querySintax)
        {
            if (HasAlias())
                return GetAlias(querySintax);
            else
                return GetFullTableName(querySintax);
        }

        private string GetAlias(IQuerySintax querySintax)
        {
            return querySintax.GetTableAlias(Alias);
        }
    }
}
