using SuperORM.Core.Domain.Model.Evaluate.Interface;
using System;

namespace SuperORM.Core.Domain.Model.Evaluate.Default
{
    public class EvaluateColumnDefault : IEvaluateColumn
    {
        public string GetEquivalentColumn(Type type, string propertyName)
        {
            return propertyName;
        }
    }
}
