using SuperORM.Core.Domain.Model.Evaluate.Interface;
using System.Collections.Generic;

namespace SuperORM.Core.Domain.Model.Evaluate
{
    public class GroupedParametersEvaluateResult : IEvaluateParameter
    {
        private readonly string _row;
        private readonly IEnumerable<IEvaluateParameter> _parameters;

        public GroupedParametersEvaluateResult(string row, IEnumerable<IEvaluateParameter> parameters)
        {
            _row = row;
            _parameters = parameters;
        }

        public object GetCompiledValue()
        {
            return _row;
        }

        public string GetContent()
        {
            return _row;
        }

        public string GetParameterAsContent()
        {
            return _row;
        }

        public object GetParameterAsRawValue()
        {
            return _row;
        }

        public string GetParameterName()
        {
            return _row;
        }

        public string GetValue()
        {
            return _row;
        }

        public bool IsNull()
        {
            return false;
        }

        public void SetNewValue(string value)
        {

        }
    }
}
