using SuperORM.Core.Domain.Service.LinqSQL.SelectableTools;
using System;
using System.Linq.Expressions;

namespace SuperORM.Core.Interface.LinqSQL
{
    public interface IInsertable<T> : IQueryBuilder
    {
        IInsertable<T> AddColumnAssimilation(ColumnAssimilator columnAssimilation);
        IInsertable<T> Into(string tableName);
        IInsertable<T> Values(params T[] values);
        IInsertable<T> Ignore(params string[] propertyName);
        IInsertable<T> Ignore(params Expression<Func<T, object>>[] columns);
        IInsertable<T> AddComplementRetrievePrimaryKey();
        int Execute();
        TResult Execute<TResult>();
    }
}
