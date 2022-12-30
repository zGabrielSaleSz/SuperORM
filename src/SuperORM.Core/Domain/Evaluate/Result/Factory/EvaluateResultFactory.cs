using SuperORM.Core.Domain.Model.Common;
using System.Collections.Generic;
using System.Linq;

namespace SuperORM.Core.Domain.Evaluate.Result.Factory
{
    public static class EvaluateResultFactory
    {
        public static IEvaluateResult AsNull()
        {
            return new EvaluateResult(SqlKeywords.NULL, true, null);
        }

        public static IEvaluateResult AsValue(string value, object compiledValue = null)
        {
            return new EvaluateResult(value.Trim(), true, compiledValue ?? value);
        }

        public static IEvaluateResult AsSqlRaw(string content, object compiledValue = null)
        {
            return new EvaluateResult(content.Trim(), false, compiledValue ?? content);
        }

        public static IEvaluateParameter AsParameter(string parameterName, IEvaluateResult content)
        {
            return new ParameterEvaluateResult(parameterName, content);
        }

        internal static IEvaluateParameter AsGroupOfParameters(IEnumerable<IEvaluateParameter> parameters)
        {
            string value = string.Join(",", parameters.Select(p => p.GetParameterName()));
            IEvaluateParameter parameterEvaluateResult = new GroupedParametersEvaluateResult(value, parameters);
            return parameterEvaluateResult;
        }
    }
}
