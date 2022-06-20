using SuperORM.Core.Domain.Model.Evaluate.Interface;
using SuperORM.Core.Domain.Model.QueryBuilder.Parameters;

namespace SuperORM.Core.Domain.Service.Evaluator.Expressions
{
    public class ConstantEvaluator : IExpressionEvaluator
    {
        private readonly IEvaluateContext Context;
        public ConstantEvaluator(IEvaluateContext context)
        {
            this.Context = context;
        }

        public IEvaluateResult Build()
        {
            object compiledResult = Context.GetQualifier().CompileAndInvoke();
            IEvaluateResult evaluateResult = CompiledValueEvaluator.Evaluate(Context.GetQuerySintax(), compiledResult);
            ParametersBuilder parametersBuilder = Context.GetParametersBuilder();
            IEvaluateParameter evaluateParameter = parametersBuilder.Add(evaluateResult, "sqlEvaluation");
            return evaluateParameter;
        }
    }
}
