using System.Linq.Expressions;

namespace SuperORM.Core.Utilities.Reflection
{
    public class ExpressionHandler<T>
    {
        public string EntityNameExpression { get; }

        public ExpressionHandler(string entityNameExpression)
        {
            EntityNameExpression = entityNameExpression;
        }

        internal ParameterExpression BuildTargetParameter()
        {
            return Expression.Parameter(typeof(T), EntityNameExpression);
        }

        internal MemberExpression BuildMemberExpressionFromProperty(string primaryKey)
        {
            return Expression.Property(BuildTargetParameter(), primaryKey);
        }
    }
}
