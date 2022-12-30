using SuperORM.Core.Domain.Service.Evaluator;
using SuperORM.Core.Domain.Service.LinqSQL.SelectableTools;
using System;
using System.Linq.Expressions;

namespace SuperORM.Core.Domain.Model.Repository
{
    public interface IPropertyConfiguration<T> where T : new()
    {
        IPropertyConfiguration<T> SetColumnName<TResult>(Expression<Func<T, TResult>> property, string columnName);
    }

    internal class PropertyConfiguration<T> : IPropertyConfiguration<T>
        where T : new()
    {
        private ColumnAssimilator<T> _columnAssimilator;
        internal PropertyConfiguration(ColumnAssimilator<T> columnAssimilator)
        {
            _columnAssimilator = columnAssimilator;
        }

        public IPropertyConfiguration<T> SetColumnName<TResult>(Expression<Func<T, TResult>> property, string columnName)
        {
            SqlExpressionEvaluator sqlEvaluator = new SqlExpressionEvaluator(property.Body);
            string propertyName = sqlEvaluator.Evaluate();
            SetColumnName(propertyName, columnName);
            return this;
        }

        private void SetColumnName(string propertyName, string columnName)
        {
            _columnAssimilator.Add(propertyName, columnName);
        }
    }
}
