using SuperORM.Core.Domain.Model.Common;
using SuperORM.Core.Domain.Model.Evaluate.Interface;
using SuperORM.Core.Utilities;
using System;
using System.Linq.Expressions;

namespace SuperORM.Core.Domain.Service.Evaluator.Expressions
{
    public class MethodEvaluator : IExpressionEvaluator
    {
        readonly IEvaluateContext Context;
        public MethodEvaluator(IEvaluateContext context)
        {
            Context = context;
        }
        public IEvaluateResult Build()
        {
            MethodCallExpression methodCallExpression = Context.GetQualifier().GetMethodExpression();

            if (methodCallExpression == null)
                throw new Exception("I've never met this Method in my life");

            if (methodCallExpression.Method.Name == "Contains")
                return HandleContainsLambda(methodCallExpression);

            throw new Exception("I've never met this Method in my life");
        }

        private IEvaluateResult HandleContainsLambda(MethodCallExpression methodCall)
        {
            ExpressionQualifier qualifier = Context.GetQualifier();
            string expression;
            if (qualifier.IsEnumerable())
            {
                Expression leftExpression = methodCall.Arguments[1];
                Expression rightExpression = methodCall.Arguments[0];

                IEvaluateResult left = ExpressionEvaluatorStrategy.Evaluate(Context.BuildChild(leftExpression));
                IEvaluateResult right = ExpressionEvaluatorStrategy.Evaluate(Context.BuildChild(rightExpression));
                expression = $"({left.GetValue()} {SqlKeywords.IN} ({right.GetContent()}))";
            }
            else
            {
                Expression leftExpression = methodCall.Object;
                Expression rightExpression = methodCall.Arguments[0];

                IEvaluateResult left = ExpressionEvaluatorStrategy.Evaluate(Context.BuildChild(leftExpression));
                IEvaluateResult right = ExpressionEvaluatorStrategy.Evaluate(Context.BuildChild(rightExpression));
                right.SetNewValue($"%{right.GetCompiledValue()}%");
                expression = $"({left.GetValue()} {SqlKeywords.LIKE} {right.GetValue()})";
            }
            return EvaluateResultFactory.AsSqlRaw(expression);
        }

        private string HandleUnaryModifier(string unaryComplement)
        {
            if (!Context.IsRoot())
            {
                ExpressionQualifier parentQualifier = Context.GetParent().GetQualifier();
                if (parentQualifier.IsUnary())
                {

                    UnaryExpression unary = parentQualifier.GetUnaryExpression();
                    unaryComplement = unary.NodeType == ExpressionType.Not ? "NOT " : "";
                }
            }

            return unaryComplement;
        }
    }
}
