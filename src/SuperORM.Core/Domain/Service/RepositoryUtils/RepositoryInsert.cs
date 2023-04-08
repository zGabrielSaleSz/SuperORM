using SuperORM.Core.Domain.Service.LinqSQL;
using SuperORM.Core.Domain.Service.LinqSQL.SelectableTools;
using SuperORM.Core.Interface.Integration;
using SuperORM.Core.Interface.LinqSQL;
using SuperORM.Core.Utilities.Reflection;

namespace SuperORM.Core.Domain.Service.RepositoryUtils
{
    internal class RepositoryInsert<Target, PrimaryKeyType>
    {
        private readonly IConnection _connection;
        private readonly IQuerySintax _querySintax;

        private ColumnAssimilator _columnAssimilator;

        internal RepositoryInsert(IConnection connection, IQuerySintax querySintax)
        {
            _connection = connection;
            _querySintax = querySintax;
            _columnAssimilator = ColumnAssimilator.Empty;
        }

        internal void AddColumnAssimilator(ColumnAssimilator columnAssimilator)
        {
            _columnAssimilator = columnAssimilator;
        }

        internal void Insert(string primaryKey, string tableName, params Target[] targets)
        {
            foreach (var target in targets)
            {
                IInsertable<Target> insertable = BuildInsertable(primaryKey, tableName, target);
                PrimaryKeyType result = insertable.Execute<PrimaryKeyType>();
                ReflectionHandler<Target> reflectionHandler = new ReflectionHandler<Target>(target);
                reflectionHandler.SetPropertyValue(primaryKey, result);
            }
        }

        internal int InsertAll(string primaryKey, string tableName, params Target[] targets)
        {
            IInsertable<Target> insertable = BuildInsertable(primaryKey, tableName, targets);
            return insertable.Execute();
        }

        private IInsertable<Target> BuildInsertable(string primaryKey, string tableName, params Target[] targets)
        {
            return new Insertable<Target>(_connection, _querySintax)
               .Ignore(primaryKey)
               .AddColumnAssimilation(_columnAssimilator)
               .Into(tableName)
               .Values(targets)
               .AddComplementRetrievePrimaryKey();
        }
    }
}
