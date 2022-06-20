using SuperORM.Core.Domain.Model.QueryBuilder;
using System.Collections.Generic;

namespace SuperORM.Core.Domain.Model.Sql
{
    public class CommandContext
    {
        public List<ParameterizedQuery> Queries { get; }
        public int CommandTimeoutSeconds { get; set; }
        public bool UseTransaction { get; set; }

        public CommandContext()
        {
            Queries = new List<ParameterizedQuery>();
            CommandTimeoutSeconds = 30;
            UseTransaction = false;
        }

        public void AddQuery(ParameterizedQuery query)
        {
            Queries.Add(query);
        }
    }
}
