using SuperORM.Core.Domain.Exceptions;
using SuperORM.Core.Domain.Model.Evaluate.Interface;
using SuperORM.Core.Domain.Service.Evaluator.Expressions;
using SuperORM.Core.Utilities;

namespace SuperORM.Core.Domain.Service.Evaluator
{
    internal class ExpressionEvaluatorStrategy
    {
        internal static IEvaluateResult Evaluate(IEvaluateContext evaluateContext)
        {
            ExpressionQualifier expressionQualifier = evaluateContext.GetQualifier();
            if (expressionQualifier.IsBinary())
            {
                return new BinaryEvaluator(evaluateContext).Build();
            }
            if (expressionQualifier.IsMemberAccess())
            {
                return new MemberAccessEvaluator(evaluateContext).Build();
            }
            if (expressionQualifier.IsMethod())
            {
                return new MethodEvaluator(evaluateContext).Build();
            }
            if (expressionQualifier.IsConstant())
            {
                return new ConstantEvaluator(evaluateContext).Build();
            }
            if (expressionQualifier.IsUnary())
            {
                return new UnaryEvaluator(evaluateContext).Build();
            }
            // class instanciation
            if (expressionQualifier.IsMemberInit())
            {

            }
            // anonymous type instanciation ?
            if (expressionQualifier.IsNew())
            {

            }
            System.Type t = expressionQualifier.GetExpressionType();
            throw new ExpressionNotSupportedException("I've never met this Expression in my life");
        }
    }
}
