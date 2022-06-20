using SuperORM.Core.Domain.Service.LinqSQL;
using SuperORM.Core.Interface;
using SuperORM.Core.Interface.LinqSQL;
using SuperORM.Core.Utilities.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SuperORM.Core.Domain.Service.Repository
{
    public class RepositoryUpdate<Target, PrimaryKeyType>
    {
        private readonly IConnection _connection;
        private readonly IQuerySintax _querySintax;

        public RepositoryUpdate(IConnection connection, IQuerySintax querySintax)
        {
            _connection = connection;
            _querySintax = querySintax;
        }

        public int Update(string primaryKey, string tableName, params Target[] targets)
        {
            int changes = 0;
            foreach (Target target in targets)
            {
                changes += UpdateTarget(primaryKey, tableName, target);
            }
            return changes;
        }

        private int UpdateTarget(string primaryKey, string tableName, Target target)
        {
            Expression<Func<Target, bool>> lambda = GetWhereExpressionByPrimaryKey(target, primaryKey);
            ReflectionHandler<Target> reflectionHandler = new ReflectionHandler<Target>(target);
            IUpdatable<Target> updatable = new Updatable<Target>(_connection, _querySintax)
                .Update(tableName);

            IEnumerable<string> properties = reflectionHandler.GetPropertiesName().Where(p => p != primaryKey);
            foreach (string property in properties)
            {
                updatable.Set(property, reflectionHandler.GetPropertyValue(property));
            }
            updatable.Where(lambda);
            return updatable.Execute();
        }

        private Expression<Func<Target, bool>> GetWhereExpressionByPrimaryKey(Target target, string primaryKeyPropertyName)
        {
            PrimaryKeyType primaryKey = ReflectionHandler<Target>.GetPropertyValue<PrimaryKeyType>(target, primaryKeyPropertyName);
            ExpressionHandler<Target> expressionHandler = new ExpressionHandler<Target>("u");

            ParameterExpression entityExpressionParameter = expressionHandler.BuildTargetParameter();

            MemberExpression primaryKeyProperty = expressionHandler.BuildMemberExpressionFromProperty(primaryKeyPropertyName);
            ConstantExpression primaryKeyValuesExpression = Expression.Constant(primaryKey);

            Expression<Func<Target, bool>> lambda = Expression.Lambda<Func<Target, bool>>(
                Expression.Equal(primaryKeyProperty, primaryKeyValuesExpression), entityExpressionParameter);
            return lambda;
        }
    }
}
