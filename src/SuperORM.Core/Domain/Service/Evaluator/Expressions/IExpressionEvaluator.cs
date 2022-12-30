using SuperORM.Core.Domain.Evaluate.Result;

namespace SuperORM.Core.Domain.Service.Evaluator.Expressions
{
    internal interface IExpressionEvaluator
    {
        IEvaluateResult Build();
    }
}
