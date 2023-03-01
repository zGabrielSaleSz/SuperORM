using SuperORM.Core.Domain.Evaluate.ColumnEvaluation;
using SuperORM.Core.Domain.Model.Enum;
using SuperORM.Core.Domain.Model.QueryBuilder.Fields;
using System;
using System.Linq.Expressions;

namespace SuperORM.Core.Interface.QueryBuilder
{
    public interface IWhereable<TEntry>
    {
        TEntry Where(IField field, IField field2);
        TEntry Where<T>(IField field, T value);
        TEntry Where<T>(IField field, SqlComparator sqlOperator, T value);
        TEntry Where(IField field, SqlComparator sqlOperator, IField field2);
        void SetWhereCondition<T>(Expression<Func<T, bool>> expression, IEvaluateColumn evaluateColumn);
    }
}
