using SuperORM.Core.Domain.Model.Evaluate.Interface;

namespace SuperORM.Core.Domain.Service.Evaluator.Expressions
{
    internal interface IExpressionEvaluator
    {
        public IEvaluateResult Build();
    }
}
