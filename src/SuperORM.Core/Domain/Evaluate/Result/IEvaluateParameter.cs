namespace SuperORM.Core.Domain.Evaluate.Result
{
    public interface IEvaluateParameter : IEvaluateResult
    {
        string GetParameterName();
        string GetParameterAsContent();
        object GetParameterAsRawValue();
    }
}
