namespace SuperORM.Core.Domain.Model.Evaluate.Interface
{
    public interface IEvaluateParameter : IEvaluateResult
    {
        string GetParameterName();
        string GetParameterAsContent();
        object GetParameterAsRawValue();
    }
}
