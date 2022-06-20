using SuperORM.Core.Domain.Model.Common;
using SuperORM.Core.Domain.Model.Evaluate.Interface;

namespace SuperORM.Core.Domain.Model.Evaluate
{
    public class EvaluateResult : IEvaluateResult
    {
        private object _rawValue;
        private string _content;
        private bool _isValue;
        public EvaluateResult(string content, bool isValue, object rawValue)
        {
            _content = content;
            _isValue = isValue;
            _rawValue = rawValue;
        }

        public string GetContent()
        {
            return _content;
        }

        public object GetCompiledValue()
        {
            return _rawValue;
        }

        public string GetValue()
        {
            if (_isValue && !IsNull())
            {
                return $"'{_content}'";
            }
            return _content;
        }

        public bool IsNull()
        {
            return (GetContent() == SqlKeywords.NULL);
        }

        public void SetNewValue(string value)
        {
            _content = value;
            _rawValue = value;
        }
    }
}
