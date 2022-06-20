namespace SuperORM.Core.Domain.Model.Evaluate.Interface
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
