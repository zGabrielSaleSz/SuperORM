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
        public string TableName;
        private Expression<Func<Target, PrimaryKeyType>> PrimaryKeyExpression;

        public readonly IConnectionProvider ConnectionProvider;
        public readonly IQuerySintax QuerySintax;

        private IConnection _connection;
        private ColumnAssimilator<Target> _columnAssimilator;
        private IPropertyConfiguration<Target> _propertyConfiguration;

        public BaseRepository(IConnectionProvider connectionProvider = null)
        {
            if(connectionProvider == null)
            {
                ConnectionProvider = Settings.Setting
                    .GetInstance()
                    .ConnectionProvider;
            }
            else
            {
                ConnectionProvider = connectionProvider;
            }

            QuerySintax = ConnectionProvider.GetQuerySintax();
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
            this.PrimaryKeyExpression = attribute;
        }

        public ISelectable<Target> Select()
        {
            return new Selectable<Target>(ConnectionProvider.GetNewConnection(), QuerySintax)
                .AddColumnAssimilation(_columnAssimilator)
                .From(TableName);
        }

        public void Insert(params Target[] targets)
        {
            RepositoryInsert<Target, PrimaryKeyType> repositoryInsert = new(GetConnection(), QuerySintax);
            repositoryInsert.AddColumnAssimilator(_columnAssimilator);
            repositoryInsert.Insert(GetPrimaryKey(), TableName, targets);
        }

        public int Delete(params Target[] targets)
        {
            RepositoryDelete<Target, PrimaryKeyType> repositoryDeletable = new(GetConnection(), QuerySintax);
            repositoryDeletable.AddColumnAssimilator(_columnAssimilator);
            return repositoryDeletable.Delete(GetPrimaryKey(), TableName, targets);
        }

        public int Update(params Target[] targets)
        {
            RepositoryUpdate<Target, PrimaryKeyType> repositoryUpdatable = new(GetConnection(), QuerySintax);
            repositoryUpdatable.AddColumnAssimilator(_columnAssimilator);
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
