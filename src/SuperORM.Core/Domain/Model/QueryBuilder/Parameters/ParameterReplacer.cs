using SuperORM.Core.Domain.Evaluate.Result;
using SuperORM.Core.Domain.Service.Evaluator;
using SuperORM.Core.Interface.Integration;

namespace SuperORM.Core.Domain.Model.QueryBuilder.Parameters
{
    public class ParameterReplacer
    {
        private readonly string _queryWithParameters;
        private readonly ParametersBuilder _parametersBuilder;
        private readonly IQuerySintax _querySintax;

        public ParameterReplacer(IQuerySintax querySintax, string queryWithParamters, ParametersBuilder parametersBuilder)
        {
            _querySintax = querySintax;
            _queryWithParameters = queryWithParamters;
            _parametersBuilder = parametersBuilder;
        }

        public string GetResult()
        {
            string queryWithoutParameters = _queryWithParameters;
            ParametersBuilder parametersBuilder = _parametersBuilder;
            foreach (string parameterName in parametersBuilder.GetParametersName())
            {
                IEvaluateParameter parameter = parametersBuilder.GetParameter(parameterName);
                string value = CompiledValueEvaluator.Evaluate(_querySintax, parameter.GetParameterAsRawValue()).GetValue();
                queryWithoutParameters = queryWithoutParameters.Replace(parameterName, value);
            }
            return queryWithoutParameters;
        }
    }
}
