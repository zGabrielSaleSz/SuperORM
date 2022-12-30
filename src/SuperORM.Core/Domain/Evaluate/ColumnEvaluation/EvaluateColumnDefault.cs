using SuperORM.Core.Domain.Evaluate.ColumnEvaluation;
using System;

namespace SuperORM.Core.Domain.Evaluate.ColumnColumnEvaluation
{
    public class EvaluateColumnDefault : IEvaluateColumn
    {
        public string GetEquivalentColumn(Type type, string propertyName)
        {
            return propertyName;
        }
    }
}
