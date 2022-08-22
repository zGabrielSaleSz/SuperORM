using System;

namespace SuperORM.Core.Domain.Model.Evaluate.Interface
{
    public interface IEvaluateColumn
    {
        string GetEquivalentColumn(Type type, string propertyName);
    }
}
