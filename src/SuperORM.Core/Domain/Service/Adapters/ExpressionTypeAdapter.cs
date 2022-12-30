using SuperORM.Core.Domain.Model.Enum;
using System.Linq.Expressions;

namespace SuperORM.Core.Domain.Service.Adapters
{
    public class ExpressionTypeAdapter
    {
        public static ExpressionType GetExpressionType(SqlComparator sqlOperator)
        {
            switch (sqlOperator)
            {
                case SqlComparator.Equal:
                    return ExpressionType.Equal;
                case SqlComparator.GreaterThan:
                    return ExpressionType.GreaterThan;
                case SqlComparator.NotEqual:
                    return ExpressionType.NotEqual;
                case SqlComparator.GreaterThanOrEqual:
                    return ExpressionType.GreaterThanOrEqual;
                case SqlComparator.LessThan:
                    return ExpressionType.LessThan;
                case SqlComparator.LessThanOrEqual:
                    return ExpressionType.LessThanOrEqual;
                default:
                    throw new System.Exception("Enum not founded");
            }
        }
    }
}
