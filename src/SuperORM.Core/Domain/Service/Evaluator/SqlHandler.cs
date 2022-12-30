using SuperORM.Core.Domain.Evaluate.Result;
using System;
using System.Linq.Expressions;

namespace SuperORM.Core.Domain.Service.Evaluator
{
    internal class SqlHandler
    {
        internal static string GetRespectiveComparator(BinaryExpression bynaryExpression, IEvaluateResult rigth)
        {
            if (rigth.IsNull())
                return GetComparatorNull(bynaryExpression.NodeType);

            return GetByNodeType(bynaryExpression.NodeType);
        }

        internal static string GetComparator<T>(ExpressionType type, T value)
        {
            if (value == null)
                return GetComparatorNull(type);
            return GetByNodeType(type);
        }

        internal static string GetByNodeType(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.NotEqual:
                    return "!=";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.And:
                    return "AND";
                case ExpressionType.Or:
                    return "OR";
                case ExpressionType.AndAlso:
                    return "AND";
                case ExpressionType.OrElse:
                    return "OR";
                case ExpressionType.Not:
                    return "NOT";
                case ExpressionType.Multiply:
                    return "*";
                case ExpressionType.Divide:
                    return "/";
                case ExpressionType.Modulo:
                    return "%";
                case ExpressionType.Add:
                    return "+";
                case ExpressionType.Subtract:
                    return "-";
                case ExpressionType.Convert:
                    return "";
                default:
                    throw new ArgumentException("", nameof(type));
            }
        }
        internal static string GetComparatorNull(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.Equal:
                    return "IS";
                case ExpressionType.NotEqual:
                    return "IS NOT";
                default:
                    throw new ArgumentException("", nameof(type));
            }
        }
    }
}
