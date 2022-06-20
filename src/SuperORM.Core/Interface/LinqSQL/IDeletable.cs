using SuperORM.Core.Interface.QueryBuilder;
using System;
using System.Linq.Expressions;

namespace SuperORM.Core.Interface.LinqSQL
{
    public interface IDeletable<T> : IQueryBuilder
    {
        IDeletable<T> From(string table);
        IDeletable<T> Where(Expression<Func<T, bool>> expression);
        int Execute();
    }
}
