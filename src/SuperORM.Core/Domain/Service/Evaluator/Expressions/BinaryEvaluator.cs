using SuperORM.Core.Domain.Evaluate.Context;
using SuperORM.Core.Domain.Evaluate.Result;
using SuperORM.Core.Domain.Evaluate.Result.Factory;
using System.Linq.Expressions;

namespace SuperORM.Core.Domain.Service.Evaluator.Expressions
{
    public class BinaryEvaluator : IExpressionEvaluator
    {
        private readonly IEvaluateContext Context;

        public BinaryEvaluator(IEvaluateContext evaluateContext)
        {
            Context = evaluateContext;
        }

        public IEvaluateResult Build()
        {
            BinaryExpression binaryExpression = Context.GetQualifier().GetBinaryExpression();

            IEvaluateResult left = ExpressionEvaluatorStrategy.Evaluate(Context.BuildChild(binaryExpression.Left));
            IEvaluateResult rigth = ExpressionEvaluatorStrategy.Evaluate(Context.BuildChild(binaryExpression.Right));

            string comparator = SqlHandler.GetRespectiveComparator(binaryExpression, rigth);
            return EvaluateResultFactory.AsSqlRaw($"({left.GetValue()} {comparator} {rigth.GetValue()})");
        }
    }
}
