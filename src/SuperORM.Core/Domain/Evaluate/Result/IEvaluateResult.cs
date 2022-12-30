namespace SuperORM.Core.Domain.Evaluate.Result
{
    public interface IEvaluateResult
    {
        string GetValue();
        string GetContent();
        object GetCompiledValue();
        void SetNewValue(string value);
        bool IsNull();
    }
}
