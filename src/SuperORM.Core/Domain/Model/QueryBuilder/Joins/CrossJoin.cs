using SuperORM.Core.Domain.Model.Common;
using SuperORM.Core.Domain.Model.QueryBuilder.Fields;
using SuperORM.Core.Interface.Integration;

namespace SuperORM.Core.Domain.Model.QueryBuilder.Joins
{
    public class CrossJoin : Join
    {
        public CrossJoin(IField fieldMainTable, IField fieldNewTable) : base(fieldMainTable, fieldNewTable) { }
        public override string GetRaw(IQuerySintax querySintax)
        {
            string raw = base.GetRaw(querySintax);
            return $"{SqlKeywords.CROSS} {raw}";
        }
    }
}
