using SuperORM.Core.Domain.Model.QueryBuilder;
using System.Collections.Generic;

namespace SuperORM.Core.Domain.Model.Sql
{
    public class ExecuteScalarResult<T>
    {
        private readonly Dictionary<ParameterizedQuery, T> _result;
        public ExecuteScalarResult()
        {
            _result = new Dictionary<ParameterizedQuery, T>();
        }
        internal void Add(ParameterizedQuery query, T result)
        {
            _result.Add(query, result);
        }

        public Dictionary<ParameterizedQuery, T> GetResult()
        {
            return _result;
        }
    }
}
