using SuperORM.Core.Domain.Model.Enum;
using System.Linq.Expressions;

namespace SuperORM.Core.Domain.Service.Adapters
{
    public class ExpressionTypeAdapter
    {
        public static ExpressionType GetExpressionType(SqlComparator sqlOperator)
        {
            return sqlOperator switch
            {
                SqlComparator.Equal => ExpressionType.Equal,
                SqlComparator.GreaterThan => ExpressionType.GreaterThan,
                SqlComparator.NotEqual => ExpressionType.NotEqual,
                SqlComparator.GreaterThanOrEqual => ExpressionType.GreaterThanOrEqual,
                SqlComparator.LessThan => ExpressionType.LessThan,
                SqlComparator.LessThanOrEqual => ExpressionType.LessThanOrEqual,
                _ => throw new System.Exception("Enum not founded"),
            };
        }
    }
}
