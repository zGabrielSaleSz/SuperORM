using SuperORM.Core.Domain.Model.QueryBuilder.Fields;

namespace SuperORM.Core.Domain.Model.LinqSQL
{
    public class JoinEvaluated
    {
        public IField FieldTable { get; set; }
        public IField FieldTableJoined { get; set; }
    }
}
