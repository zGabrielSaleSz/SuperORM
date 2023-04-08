using SuperORM.Core.Interface.LinqSQL;
using SuperORM.Core.Interface.Repository;
using System.Linq.Expressions;
using System;
using SuperORM.Core.Domain.Service.LinqSQL;
using SuperORM.Core.Interface.Integration;
using SuperORM.Core.Domain.Service.LinqSQL.SelectableTools;
using System.Xml.Linq;

namespace SuperORM.Core.Domain.Service.Repository
{
    public class SelectableRepository<T> : Selectable<T>, ISelectableRepository<T> where T : new()
    {
        public SelectableRepository(string tableName, IConnection connection, IQuerySintax querySintax) : base(connection, querySintax)
        {
            From(tableName);
        }

        public ISelectableRepository<T> InnerJoin<T2>(Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined, string alias = "")
        {
            InnerJoin<T2>(GetTableOfType<T2>(), attributeRoot, attributeJoined, alias);
            return this;
        }

        public ISelectableRepository<T> InnerJoin<T1, T2>(Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined, string alias = "")
        {
            InnerJoin(GetTableOfType<T2>(), attributeRoot, attributeJoined, alias);
            return this;
        }

        public ISelectableRepository<T> LeftJoin<T2>(Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined)
        {
            LeftJoin<T2>(GetTableOfType<T2>(), attributeRoot, attributeJoined);
            return this;
        }

        public ISelectableRepository<T> LeftJoin<T1, T2>(Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined)
        {
            LeftJoin(GetTableOfType<T2>(), attributeRoot, attributeJoined);
            return this;
        }

        //
        public ISelectableRepository<T> RightJoin<T2>(Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined)
        {
            RightJoin<T2>(GetTableOfType<T2>(), attributeRoot, attributeJoined);
            return this;
        }

        public ISelectableRepository<T> RightJoin<T1, T2>(Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined)
        {
            RightJoin(GetTableOfType<T2>(), attributeRoot, attributeJoined);
            return this;
        }

        public ISelectableRepository<T> CrossJoin<T2>(Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined)
        {
            CrossJoin<T2>(GetTableOfType<T2>(), attributeRoot, attributeJoined);
            return this;
        }

        public ISelectableRepository<T> CrossJoin<T1, T2>(Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined)
        {
            CrossJoin(GetTableOfType<T2>(), attributeRoot, attributeJoined);
            return this;
        }

        public ISelectableRepository<T> FullJoin<T2>(Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined)
        {
            FullJoin<T2>(GetTableOfType<T2>(), attributeRoot, attributeJoined);
            return this;
        }

        public ISelectableRepository<T> FullJoin<T1, T2>(Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined)
        {
            FullJoin(GetTableOfType<T2>(), attributeRoot, attributeJoined);
            return this;
        }

        public ISelectableRepository<T> SelfJoin<T2>(Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined)
        {
            SelfJoin<T2>(GetTableOfType<T2>(), attributeRoot, attributeJoined);
            return this;
        }

        public ISelectableRepository<T> SelfJoin<T1, T2>(Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined)
        {
            SelfJoin(GetTableOfType<T2>(), attributeRoot, attributeJoined);
            return this;
        }

        public new ISelectableRepository<T> UseRepositoryRegistry(IRepositoryRegistry repositoryRegistry)
        {
            UseRepositoryRegistry(repositoryRegistry);
            return this;
        }

        public new ISelectableRepository<T> Select(params Expression<Func<T, object>>[] attributes)
        {
            base.Select(attributes);
            return this;
        }

        public new ISelectableRepository<T> Select<T2>(params Expression<Func<T2, object>>[] attributes)
        {
            base.Select(attributes);
            return this;
        }

        public new ISelectableRepository<T> Where(Expression<Func<T, bool>> expression)
        {
            base.Where(expression);
            return this;
        }

        public ISelectableRepository<T> OrderBy(params Expression<Func<T, object>>[] attributes)
        {
            base.OrderBy(attributes);
            return this;
        }

        public new ISelectableRepository<T> OrderBy<T1>(params Expression<Func<T1, object>>[] attributes)
        {
            base.OrderBy(attributes);
            return this;
        }

        public new ISelectableRepository<T> OrderByDescending<T1>(params Expression<Func<T1, object>>[] attributes)
        {
            base.OrderByDescending(attributes);
            return this;
        }

        public new ISelectableRepository<T> OrderByDescending(params Expression<Func<T, object>>[] attributes)
        {
            base.OrderByDescending(attributes);
            return this;
        }

        public ISelectableRepository<T> AddColumnAssimilation(ColumnAssimilator columnAssimilation)
        {
            base.AddColumnAssimilation(columnAssimilation);
            return this;
        }
    }
}
