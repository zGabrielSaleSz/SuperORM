using SuperORM.Core.Domain.Service.LinqSQL;
using SuperORM.Core.Domain.Service.LinqSQL.SelectableTools;
using SuperORM.Core.Interface.Integration;
using SuperORM.Core.Interface.LinqSQL;
using SuperORM.Core.Interface.Repository;
using System;
using System.Linq.Expressions;

namespace SuperORM.Core.Domain.Service.Repository
{
    public class RepositorySelect<Target, PrimaryKeyType> where Target : new()
    {
        private readonly ISelectable<Target> _selectable;
        private readonly IConnection _connection;
        private readonly IQuerySintax _querySintax;

        private ColumnAssimilator _columnAssimilator;

        internal RepositorySelect(IConnection connection, IQuerySintax querySintax, IRepositoryRegistry repositoryRegistry)
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
            throw new NotImplementedException();
        }
    }
}
