using SuperORM.Core.Domain.Service.LinqSQL.SelectableTools;
using System;
using System.Linq.Expressions;

namespace SuperORM.Core.Interface.LinqSQL
{
    public interface IUpdatable<T> : IQueryBuilder
    {
        IUpdatable<T> AddColumnAssimilation(ColumnAssimilator columnAssimilation);
        IUpdatable<T> Update(string tableName);
        IUpdatable<T> Set<TResult>(Expression<Func<T, TResult>> attribute, TResult value);
        IUpdatable<T> Set<TResult>(Expression<Func<T, TResult>> attribute, Expression<Func<T, TResult>> attribute2);
        IUpdatable<T> Set<TResult>(string attributeName, TResult value);
        IUpdatable<T> Where(Expression<Func<T, bool>> expression);
        int Execute();
    }
}
