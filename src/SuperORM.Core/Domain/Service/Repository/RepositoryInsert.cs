using SuperORM.Core.Domain.Model.Sql;
using SuperORM.Core.Domain.Service.LinqSQL;
using SuperORM.Core.Interface;
using SuperORM.Core.Interface.LinqSQL;
using SuperORM.Core.Utilities.Reflection;

namespace SuperORM.Core.Domain.Service.Repository
{
    public class RepositoryInsert<Target, PrimaryKeyType>
    {
        private readonly IConnection _connection;
        private readonly IQuerySintax _querySintax;

        public RepositoryInsert(IConnection connectionProvider, IQuerySintax querySintax)
        {
            _connection = connectionProvider;
            _querySintax = querySintax;
        }

        public void Insert(string primaryKey, string tableName, params Target[] targets)
        {
            foreach (var target in targets)
            {
                IInsertable<Target> insertable = BuildInsertable(primaryKey, tableName, target);
                PrimaryKeyType result = insertable.Execute<PrimaryKeyType>();
                ReflectionHandler<Target> reflectionHandler = new ReflectionHandler<Target>(target);
                reflectionHandler.SetPropertyValue(primaryKey, result);
            }
        }

        public int InsertAll(string primaryKey, string tableName, params Target[] targets)
        {
            IInsertable<Target> insertable = BuildInsertable(primaryKey, tableName, targets);
            return insertable.Execute();
        }

        private IInsertable<Target> BuildInsertable(string primaryKey, string tableName, params Target[] targets)
        {
            
            return new Insertable<Target>(_connection, _querySintax)
               .Ignore(primaryKey)
               .Into(tableName)
               .Values(targets)
               .AddComplementRetrievePrimaryKey();
        }


    }
}
