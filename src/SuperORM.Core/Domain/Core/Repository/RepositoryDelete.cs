using SuperORM.Core.Domain.Service.LinqSQL;
using SuperORM.Core.Domain.Service.LinqSQL.SelectableTools;
using SuperORM.Core.Interface;
using SuperORM.Core.Interface.LinqSQL;
using SuperORM.Core.Utilities.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SuperORM.Core.Domain.Service.Repository
{
    internal class RepositoryDelete<Target, PrimaryKeyType>
    {
        private readonly IConnection _connection;
        private readonly IQuerySintax _querySintax;

        private ColumnAssimilator _columnAssimilator;

        internal RepositoryDelete(IConnection connection, IQuerySintax querySintax)
        {
            _connection = connection;
            _querySintax = querySintax;
            _columnAssimilator = ColumnAssimilator.Empty;
        }
        internal void AddColumnAssimilator(ColumnAssimilator columnAssimilator)
        {
            _columnAssimilator = columnAssimilator;
        }

        internal int Delete(string primaryKey, string tableName, params Target[] targets)
        {
            Expression<Func<Target, bool>> lambda = GetWhereExpressionByPrimaryKey(primaryKey, targets);
            IDeletable<Target> deletable = new Deletable<Target>(_connection, _querySintax)
                .AddColumnAssimilation(_columnAssimilator)
                .From(tableName)
                .Where(lambda);

            return deletable.Execute();
        }

        private Expression<Func<Target, bool>> GetWhereExpressionByPrimaryKey(string primaryKeyPropertyName, Target[] targets)
        {
            List<PrimaryKeyType> primaryKeyValues = ReflectionHandler<Target>.GetValuesByProperty<PrimaryKeyType>(targets, primaryKeyPropertyName).ToList();

            MethodInfo methodContains = ReflectionUtils.BuildEnumerableGenericMethod<PrimaryKeyType>("Contains", 2);

            ExpressionHandler<Target> expressionHandler = new ExpressionHandler<Target>("e");
            ParameterExpression entityExpressionParameter = expressionHandler.BuildTargetParameter();

            ConstantExpression primaryKeyValuesExpression = Expression.Constant(primaryKeyValues);
            MemberExpression primaryKeyProperty = expressionHandler.BuildMemberExpressionFromProperty(primaryKeyPropertyName);
            MethodCallExpression call = Expression.Call(methodContains, primaryKeyValuesExpression, primaryKeyProperty);

            Expression<Func<Target, bool>> lambda = Expression.Lambda<Func<Target, bool>>(call, entityExpressionParameter);
            return lambda;
        }
    }
}
