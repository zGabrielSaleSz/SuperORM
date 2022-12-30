using System;

namespace SuperORM.Core.Domain.Evaluate.ColumnEvaluation
{
    public interface IEvaluateColumn
    {
        string GetEquivalentColumn(Type type, string propertyName);
    }
}
