using SuperORM.Core.Domain.Model.Common;
using SuperORM.Core.Domain.Model.Evaluate.Interface;

namespace SuperORM.Core.Domain.Model.Evaluate
{
    public class ParameterEvaluateResult : IEvaluateParameter
    {
        private readonly string _parameterName;
        private readonly IEvaluateResult _content;
        private bool _isValue;

        public ParameterEvaluateResult(string parameterName, IEvaluateResult content)
        {
            _parameterName = parameterName;
            _content = content;
            _isValue = false;
        }

        public string GetContent()
        {
            _isValue = false;
            return _parameterName;
        }

        public string GetValue()
        {
            _isValue = true;
            return _parameterName;
        }

        public bool IsNull()
        {
            return (_content.GetContent() == SqlKeywords.NULL);
        }

        public string GetParameterAsContent()
        {
            if (_isValue)
            {
                return _content.GetValue();
            }
            return _content.GetContent();
        }

        public object GetParameterAsRawValue()
        {
            return GetCompiledValue();
        }

        public string GetParameterName()
        {
            return _parameterName;
        }

        public object GetCompiledValue()
        {
            return _content.GetCompiledValue();
        }

        public void SetNewValue(string value)
        {
            _content.SetNewValue(value);
        }
    }
}
