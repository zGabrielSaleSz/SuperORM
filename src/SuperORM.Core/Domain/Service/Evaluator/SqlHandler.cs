using SuperORM.Core.Domain.Model.Evaluate.Interface;
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
            return type switch
            {
                ExpressionType.Equal => "=",
                ExpressionType.NotEqual => "!=",
                ExpressionType.GreaterThan => ">",
                ExpressionType.GreaterThanOrEqual => ">=",
                ExpressionType.LessThan => "<",
                ExpressionType.LessThanOrEqual => "<=",
                ExpressionType.And => "AND",
                ExpressionType.Or => "OR",
                ExpressionType.AndAlso => "AND",
                ExpressionType.OrElse => "OR",
                ExpressionType.Not => "NOT",
                ExpressionType.Multiply => "*",
                ExpressionType.Divide => "/",
                ExpressionType.Modulo => "%",
                ExpressionType.Add => "+",
                ExpressionType.Subtract => "-",

                // Ignored ones
                ExpressionType.Convert => "",
                _ => throw new ArgumentException("", nameof(type)),
            };
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
