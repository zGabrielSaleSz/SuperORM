using SuperORM.Core.Domain.Model.Evaluate.Interface;
using SuperORM.Core.Domain.Service;
using SuperORM.Core.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace SuperORM.Core.Domain.Model.QueryBuilder.Parameters
{
    public class ParametersBuilder
    {
        private readonly Dictionary<string, IEvaluateParameter> _parameters;
        private uint _count;

        public ParametersBuilder()
        {
            _parameters = new Dictionary<string, IEvaluateParameter>();
            _count = 0;
        }

        public IEvaluateParameter Add(IEvaluateResult value, string tip = "")
        {
            TypeQualifier typeQualifier = new TypeQualifier(value.GetCompiledValue());
            if (typeQualifier.IsEnumerable())
            {
                return BuildParametersGroup(typeQualifier.GetAsEnumerable().Cast<object>());
            }
            else
            {
                return BuildParameter(value, tip);
            }
        }

        private IEvaluateParameter BuildParameter(IEvaluateResult value, string tip)
        {
            string key = GenerateKey(tip);
            IEvaluateParameter evaluateParameterResult = EvaluateResultFactory.AsParameter(key, value);
            _parameters.Add(key, evaluateParameterResult);
            return evaluateParameterResult;
        }

        private IEvaluateParameter BuildParametersGroup(IEnumerable<object> parameters)
        {
            List<IEvaluateParameter> subParameters = new List<IEvaluateParameter>();
            foreach (object parameter in parameters)
            {
                var subParameter = Add(EvaluateResultFactory.AsSqlRaw(parameter.ToString(), parameter));
                subParameters.Add(subParameter);
            }
            return EvaluateResultFactory.AsGroupOfParameters(subParameters);
        }

        private string GenerateKey(string tip)
        {
            return $"@param{_count++}{tip}";
        }

        public IEvaluateParameter GetParameter(string key)
        {
            if (!_parameters.ContainsKey(key))
                throw new KeyNotFoundException();

            return _parameters[key];
        }

        public IEnumerable<string> GetParametersName()
        {
            return _parameters.Keys.AsEnumerable();
        }

        public Dictionary<string, object> GetParameters()
        {
            return _parameters.ToDictionary(k => k.Key, v => (object)v.Value.GetParameterAsRawValue());
        }
    }
}
