using SuperORM.Core.Domain.Evaluate.Context;
using SuperORM.Core.Domain.Evaluate.Result;
using SuperORM.Core.Domain.Evaluate.Result.Factory;
using System.Linq.Expressions;

namespace SuperORM.Core.Domain.Service.Evaluator.Expressions
{
    public class UnaryEvaluator : IExpressionEvaluator
    {
        readonly IEvaluateContext Context;


        public UnaryEvaluator(IEvaluateContext context)
        {
            this.Context = context;
        }

        public IEvaluateResult Build()
        {
            UnaryExpression unaryExpression = Context.GetQualifier().GetUnaryExpression();
            string innerOperator = SqlHandler.GetByNodeType(unaryExpression.NodeType);
            return EvaluateResultFactory.AsSqlRaw($"{innerOperator} {ExpressionEvaluatorStrategy.Evaluate(Context.BuildChild(unaryExpression.Operand)).GetContent()}");
        }
    }
}
