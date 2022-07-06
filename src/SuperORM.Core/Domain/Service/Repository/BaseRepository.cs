using SuperORM.Core.Domain.Model.Sql;
using SuperORM.Core.Domain.Service.LinqSQL;
using SuperORM.Core.Interface;
using SuperORM.Core.Interface.Repository;
using System;
using System.Linq.Expressions;

namespace SuperORM.Core.Domain.Service.Repository
{
    public abstract class BaseRepository<Target, PrimaryKeyType> : IBaseRepository where Target : new()
    {
        public string TableName;
        private Expression<Func<Target, PrimaryKeyType>> PrimaryKeyExpression;

        public readonly IConnectionProvider ConnectionProvider;
        public readonly IQuerySintax QuerySintax;
        private IConnection _connection;

        public BaseRepository()
        {
            var settings = Settings.Setting.GetInstance();

            ConnectionProvider = settings.ConnectionProvider;
            QuerySintax = settings.QuerySintax;

            Configurate();
        }
        public abstract void Configurate();

        public void SetPrimaryKey(Expression<Func<Target, PrimaryKeyType>> attribute)
        {
            this.PrimaryKeyExpression = attribute;
        }

        public ISelectable<Target> Select()
        {
            return new Selectable<Target>(ConnectionProvider.GetNewConnection(), QuerySintax)
                .From(TableName);
        }

        public void Insert(params Target[] targets)
        {
            RepositoryInsert<Target, PrimaryKeyType> repositoryInsert = new(GetConnection(), QuerySintax);
            repositoryInsert.Insert(GetPrimaryKey(), TableName, targets);
        }

        public int Delete(params Target[] targets)
        {
            RepositoryDelete<Target, PrimaryKeyType> repositoryDeletable = new(GetConnection(), QuerySintax);
            return repositoryDeletable.Delete(GetPrimaryKey(), TableName, targets);
        }

        public int Update(params Target[] targets)
        {
            RepositoryUpdate<Target, PrimaryKeyType> repositoryUpdatable = new(GetConnection(), QuerySintax);
            return repositoryUpdatable.Update(GetPrimaryKey(), TableName, targets);

        }

        private string GetPrimaryKey()
        {
            SqlExpressionEvaluator sqlEvaluator = new SqlExpressionEvaluator(PrimaryKeyExpression.Body, QuerySintax);
            string primaryKey = sqlEvaluator.Evaluate();
            return primaryKey;
        }

        public void SetTable(string tableName)
        {
            this.TableName = tableName;
        }

        public void UseConnection(IConnection connection)
        {
            _connection = connection;
        }

        private IConnection GetConnection()
        {
            if (_connection == null)
                return ConnectionProvider.GetNewConnection();
            else
                return _connection;
        }
    }
}
