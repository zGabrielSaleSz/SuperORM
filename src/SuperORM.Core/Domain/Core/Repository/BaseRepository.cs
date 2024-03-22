using SuperORM.Core.Domain.Model.Repository;
using SuperORM.Core.Domain.Service.LinqSQL;
using SuperORM.Core.Domain.Service.LinqSQL.SelectableTools;
using SuperORM.Core.Interface;
using SuperORM.Core.Interface.Repository;
using System;
using System.Linq.Expressions;

namespace SuperORM.Core.Domain.Service.Repository
{
    public abstract class BaseRepository<Target, PrimaryKeyType> : IBaseRepository where Target : new()
    {
        public string TableName { get; private set; }

        private Expression<Func<Target, PrimaryKeyType>> _primaryKeyExpression;
        private IConnectionProvider _connectionProvider;
        private IConnection _connection;

        private ColumnAssimilator<Target> _columnAssimilator;
        private IPropertyConfiguration<Target> _propertyConfiguration;

        public BaseRepository(IConnectionProvider connectionProvider = null)
        {
            if(connectionProvider != null)
            {
                _connectionProvider = connectionProvider;
            }
            _columnAssimilator = new ColumnAssimilator<Target>();
            _propertyConfiguration = new PropertyConfiguration<Target>(_columnAssimilator);

            Configurate();
            ConfigurateColumns(_propertyConfiguration);
        }

        public abstract void Configurate();
        public virtual void ConfigurateColumns(IPropertyConfiguration<Target> propertyConfiguration)
        {

        }

        public void SetPrimaryKey(Expression<Func<Target, PrimaryKeyType>> attribute)
        {
            this._primaryKeyExpression = attribute;
        }

        public void SetRelationShip<T2>(Expression<Func<Target, object>> attribute, Expression<Func<Target, T2>> targetExpression, Expression<Func<T2, object>> targetAttribute)
        {

        }

        public ISelectable<Target> Select()
        {
            return new Selectable<Target>(GetConnectionProvider().GetNewConnection(), GetQuerySintax())
                .AddColumnAssimilation(_columnAssimilator)
                .From(TableName);
        }

        public void Insert(params Target[] targets)
        {
            RepositoryInsert<Target, PrimaryKeyType> repositoryInsert = new RepositoryInsert<Target, PrimaryKeyType>(GetConnection(), GetQuerySintax());
            repositoryInsert.AddColumnAssimilator(_columnAssimilator);
            repositoryInsert.Insert(GetPrimaryKey(), TableName, targets);
        }

        public int Delete(params Target[] targets)
        {
            RepositoryDelete<Target, PrimaryKeyType> repositoryDeletable = new RepositoryDelete<Target, PrimaryKeyType>(GetConnection(), GetQuerySintax());
            repositoryDeletable.AddColumnAssimilator(_columnAssimilator);
            return repositoryDeletable.Delete(GetPrimaryKey(), TableName, targets);
        }

        public int Update(params Target[] targets)
        {
            RepositoryUpdate<Target, PrimaryKeyType> repositoryUpdatable = new RepositoryUpdate<Target, PrimaryKeyType>(GetConnection(), GetQuerySintax());
            repositoryUpdatable.AddColumnAssimilator(_columnAssimilator);
            return repositoryUpdatable.Update(GetPrimaryKey(), TableName, targets);
        }

        private string GetPrimaryKey()
        {
            SqlExpressionEvaluator sqlEvaluator = new SqlExpressionEvaluator(_primaryKeyExpression.Body, GetQuerySintax());
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

        public void UseConnectionProvider(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        private IConnectionProvider GetConnectionProvider()
        {
            if (_connectionProvider != null)
                return _connectionProvider;

            return Settings.Setting.GetInstance().ConnectionProvider;
        }

        private IConnection GetConnection()
        {
            if (_connection != null)
                return _connection;
            return GetConnectionProvider().GetNewConnection();
        }

        private IQuerySintax GetQuerySintax()
        {
            return GetConnectionProvider().GetQuerySintax();
        }

        public string GetTableName()
        {
            return TableName;
        }
    }
}
