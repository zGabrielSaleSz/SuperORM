using SuperORM.Core.Domain.Exceptions;
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

        public IConnectionProvider ConnectionProvider { get; private set; }

        private IConnection _connection;
        private Expression<Func<Target, PrimaryKeyType>> _primaryKeyExpression;

        private readonly ColumnAssimilator<Target> _columnAssimilator;
        private readonly IPropertyConfiguration<Target> _propertyConfiguration;
        private readonly Type _targetType;

        public BaseRepository(IConnectionProvider connectionProvider)
        {
            ConnectionProvider = connectionProvider;
            _columnAssimilator = new ColumnAssimilator<Target>();
            _propertyConfiguration = new PropertyConfiguration<Target>(_columnAssimilator);
            _targetType = typeof(Target);

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

        public void SetTable(string tableName)
        {
            TableName = tableName;
        }

        public string GetTableName()
        {
            return TableName;
        }

        public Type GetTargetType()
        {
            return _targetType;
        }

        

        public ISelectable<Target> GetSelectable()
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

        public void UseConnection(IConnection connection)
        {
            _connection = connection;
        }

        private IConnectionProvider GetConnectionProvider()
        {
            if (ConnectionProvider == null)
                throw new MissingConnectionProviderException("Please setup the ConnectionProvider through the constructor or using the method");

            return ConnectionProvider;
        }

        private IConnection GetConnection()
        {
            if (_connection == null)
                return GetConnectionProvider().GetNewConnection();
            else
                return _connection;
        }

        private IQuerySintax GetQuerySintax()
        {
            return GetConnectionProvider().GetQuerySintax();
        }

        private string GetPrimaryKey()
        {
            SqlExpressionEvaluator sqlEvaluator = new SqlExpressionEvaluator(_primaryKeyExpression.Body, GetQuerySintax());
            string primaryKey = sqlEvaluator.Evaluate();
            return primaryKey;
        }
    }
}
