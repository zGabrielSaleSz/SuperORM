using SuperORM.Core.Domain.Model.Evaluate.Interface;
using SuperORM.Core.Domain.Model.QueryBuilder.Parameters;
using SuperORM.Core.Utilities;
using System;
using System.Linq.Expressions;

namespace SuperORM.Core.Domain.Service.Evaluator.Expressions
{
    public class MemberAccessEvaluator : IExpressionEvaluator
    {
        readonly IEvaluateContext Context;
        public MemberAccessEvaluator(IEvaluateContext context)
        {
            this.Context = context;
        }

        public IEvaluateResult Build()
        {
            ExpressionQualifier expressionQualifier = Context.GetQualifier();
            MemberExpression memberExpression = expressionQualifier.GetMemberExpression();
            Expression child = memberExpression.Expression;

            if (ExpressionQualifier.Of(child).IsParameter())
            {
                Type typeOf = memberExpression.Expression.Type;
                IEvaluateColumn evaluateColumn = Context.GetEvaluateColumn();
                string columnName = evaluateColumn.GetEquivalentColumn(typeOf, memberExpression.Member.Name);
                return EvaluateResultFactory.AsSqlRaw(columnName);
            }

            object compiledResult = ExpressionQualifier.Of(memberExpression).CompileAndInvoke();
            IEvaluateResult evaluateResult = CompiledValueEvaluator.Evaluate(Context.GetQuerySintax(), compiledResult);

            ParametersBuilder parametersBuilder = Context.GetParametersBuilder();
            IEvaluateParameter parameter = parametersBuilder.Add(evaluateResult, "sqlEvaluation");

            return parameter;
        }
    }
}
