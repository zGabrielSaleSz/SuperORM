using SuperORM.Core.Domain.Model.LinqSQL;
using SuperORM.Core.Domain.Service.LinqSQL.SelectableTools;
using SuperORM.Core.Interface.LinqSQL;
using SuperORM.Core.Interface.QueryBuilder;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SuperORM.Core.Interface.Repository
{
    public interface ISelectableRepository<T> : IQueryBuilder where T : new()
    {
        ISelectableRepository<T> AddColumnAssimilation(ColumnAssimilator columnAssimilation);
        ISelectableRepository<T> UseRepositoryRegistry(IRepositoryRegistry repositoryRegistry);

        ISelectableRepository<T> Select(params Expression<Func<T, object>>[] attributes);
        ISelectableRepository<T> Select<T2>(params Expression<Func<T2, object>>[] attributes);


        ISelectableRepository<T> InnerJoin<T2>(Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined, string alias = "");
        ISelectableRepository<T> InnerJoin<T1, T2>(Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined, string alias = "");

        ISelectableRepository<T> LeftJoin<T2>(Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);
        ISelectableRepository<T> LeftJoin<T1, T2>(Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);

        ISelectableRepository<T> RightJoin<T2>(Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);
        ISelectableRepository<T> RightJoin<T1, T2>(Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);

        ISelectableRepository<T> CrossJoin<T2>(Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);
        ISelectableRepository<T> CrossJoin<T1, T2>(Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);

        ISelectableRepository<T> FullJoin<T2>(Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);
        ISelectableRepository<T> FullJoin<T1, T2>(Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);

        ISelectableRepository<T> SelfJoin<T2>(Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);
        ISelectableRepository<T> SelfJoin<T1, T2>(Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined);

        ISelectableRepository<T> Where(Expression<Func<T, bool>> expression);

        ISelectableRepository<T> OrderBy(params Expression<Func<T, object>>[] attributes);
        ISelectableRepository<T> OrderBy<T1>(params Expression<Func<T1, object>>[] attributes);

        ISelectableRepository<T> OrderByDescending(params Expression<Func<T, object>>[] attributes);
        ISelectableRepository<T> OrderByDescending<T1>(params Expression<Func<T1, object>>[] attributes);

        IEnumerable<ResultPicker> GetResult();
    }
}
