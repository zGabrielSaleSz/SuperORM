using System.Collections.Generic;

namespace SuperORM.Core.Domain.Model.QueryBuilder
{
    public class ParameterizedQuery
    {
        public readonly string Query;
        public readonly Dictionary<string, object> Parameters;
        public ParameterizedQuery(string query, Dictionary<string, object> parameters)
        {
            this.Query = query;
            this.Parameters = parameters;
        }
    }
}
