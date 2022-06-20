using System.Collections.Generic;

namespace SuperORM.Core.Domain.Model.QueryBuilder.Fields
{
    public interface IFieldsBuilder
    {
        IFieldsBuilder AddField(string fieldName, string fieldAlias = null);
        IFieldsBuilder AddField<T>(string fieldName, string fieldAlias = null) where T : IFieldArgument, new();
        IEnumerable<IField> GetResult();
        IField Find(string fieldName);
    }
}
