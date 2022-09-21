using SuperORM.Core.Domain.Service.LinqSQL;
using SuperORM.Core.Domain.Service.LinqSQL.SelectableTools;
using SuperORM.Core.Interface;
using SuperORM.Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SuperORM.Core.Domain.Service.Repository
{
    public class RepositorySelect<Target, PrimaryKeyType> where Target : new()
    {
        private readonly ISelectable<Target> _selectable;
        private readonly IConnection _connection;
        private readonly IQuerySintax _querySintax;

        private ColumnAssimilator _columnAssimilator;

        internal RepositorySelect(IConnection connection, IQuerySintax querySintax)
        {
            _connection = connection;
            _querySintax = querySintax;
            _selectable = new Selectable<Target>(connection, querySintax);
            _columnAssimilator = ColumnAssimilator.Empty;
        }

        internal void AddColumnAssimilator(ColumnAssimilator columnAssimilator)
        {
            _columnAssimilator = columnAssimilator;
        }

        public RepositorySelect<Target, PrimaryKeyType> Include<T, T2>(T joinEntity, Expression<Func<T, T2>> attribute)
        {
            IBaseRepository baseRepository = RepositoryRegistry.GetInstance().GetRepository(typeof(T));
            //_selectable.InnerJoin<T>(baseRepository.GetTableName(), );
            //attribute.
            return null;
        }
    }
}
