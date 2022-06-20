using SuperORM.Core.Domain.Model.QueryBuilder;

namespace SuperORM.Core.Domain.Model.Sql
{
    public class CommandReaderContext
    {
        public ParameterizedQuery Query { get; set; }
        public int CommandTimeoutSeconds { get; set; }
        public CommandReaderContext(ParameterizedQuery query)
        {
            Query = query;
            CommandTimeoutSeconds = 30;
        }
    }
}
