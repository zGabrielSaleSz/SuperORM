using SuperORM.Core.Domain.Model.Evaluate.Interface;
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
