using SuperORM.Core.Domain.Model.LinqSQL;
using SuperORM.Core.Domain.Service.LinqSQL.SelectableTools;
using SuperORM.Core.Interface.QueryBuilder;
using SuperORM.Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SuperORM.Core.Interface
{
    public interface ISelectable<T> : IQueryBuilder
    {
        ISelectable<T> AddColumnAssimilation(ColumnAssimilator columnAssimilation);
        //ISelectable<T2> Select<T2, TResult>(this ISelectable<T2> query, Expression<Func<T2, TResult>> expression);
        ISelectable<T> From(string tableName);
        ISelectable<T> From<T2>();
        ISelectable<T> SelectAll();
        ISelectable<T> Select(string field);
        ISelectable<T> Select(params Expression<Func<T, object>>[] attributes);
        ISelectable<T> Select<T2>(params Expression<Func<T2, object>>[] attributes);
        ISelectable<T> Top(uint rows);
        ISelectable<T> Skip(uint rows);
        ISelectable<T> Take(uint rows);

        ISelectable<T> InnerJoin<T2>(Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);
        ISelectable<T> InnerJoin<T1, T2>(Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);
        ISelectable<T> InnerJoin<T2>(string tableName, Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);
        ISelectable<T> InnerJoin<T1, T2>(string tableName, Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);

        ISelectable<T> LeftJoin<T2>(Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);
        ISelectable<T> LeftJoin<T1, T2>(Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);
        ISelectable<T> LeftJoin<T2>(string tableName, Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);
        ISelectable<T> LeftJoin<T1, T2>(string tableName, Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);

        ISelectable<T> RightJoin<T2>(Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);
        ISelectable<T> RightJoin<T1, T2>(Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);
        ISelectable<T> RightJoin<T2>(string tableName, Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);
        ISelectable<T> RightJoin<T1, T2>(string tableName, Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);

        ISelectable<T> CrossJoin<T2>(Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);
        ISelectable<T> CrossJoin<T1, T2>(Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);
        ISelectable<T> CrossJoin<T2>(string tableName, Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);
        ISelectable<T> CrossJoin<T1, T2>(string tableName, Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);

        ISelectable<T> FullJoin<T2>(Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);
        ISelectable<T> FullJoin<T1, T2>(Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);
        ISelectable<T> FullJoin<T2>(string tableName, Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);
        ISelectable<T> FullJoin<T1, T2>(string tableName, Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);

        ISelectable<T> SelfJoin<T2>(Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);
        ISelectable<T> SelfJoin<T1, T2>(Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);
        ISelectable<T> SelfJoin<T2>(string tableName, Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);
        ISelectable<T> SelfJoin<T1, T2>(string tableName, Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);

        ISelectable<T> Where(Expression<Func<T, bool>> expression);
        ISelectable<T> GroupBy(params Expression<Func<T, object>>[] attributes);
        ISelectable<T> GroupBy<T1>(params Expression<Func<T1, object>>[] attributes);
        ISelectable<T> OrderBy(params Expression<Func<T, object>>[] attributes);
        ISelectable<T> OrderBy<T1>(params Expression<Func<T1, object>>[] attributes);
        ISelectable<T> OrderByDescending(params Expression<Func<T, object>>[] attributes);
        ISelectable<T> OrderByDescending<T1>(params Expression<Func<T1, object>>[] attributes);
        ISelectable<T> Limit(uint rows);
        ISelectable<T> Limit(uint startIndex, uint amount);
        IEnumerable<T> AsEnumerable();
        IEnumerable<ResultPicker> GetResult();
        T FirstOrDefault();
        string GetTableOfType<T1>();
    }
}
