using SuperORM.Core.Domain.Exceptions;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SuperORM.Core.Utilities
{
    public class ExpressionQualifier
    {
        private readonly Expression _expression;
        public ExpressionQualifier(Expression expression)
        {
            _expression = expression;
        }

        public static ExpressionQualifier Of(Expression expression)
        {
            ExpressionQualifier common = new ExpressionQualifier(expression);
            return common;
        }

        public Type GetExpressionType()
        {
            return _expression.Type;
        }

        public bool IsBinary()
        {
            return _expression is BinaryExpression;
        }

        public BinaryExpression GetBinaryExpression()
        {
            if (!IsBinary())
                throw new ExpressionQualifiedException("Not a binary expression");
            return _expression as BinaryExpression;
        }

        public bool IsMethod()
        {
            return _expression is MethodCallExpression;
        }

        public bool IsUnary()
        {
            return _expression is UnaryExpression;
        }

        public UnaryExpression GetUnaryExpression()
        {
            return _expression as UnaryExpression;
        }

        public MethodCallExpression GetMethodExpression()
        {
            if (!IsMethod())
                throw new ExpressionQualifiedException("Not a method expression");
            return _expression as MethodCallExpression;
        }

        public MemberExpression GetMemberExpression()
        {
            return _expression as MemberExpression;
        }

        public bool IsMemberAccess()
        {
            return _expression.NodeType == ExpressionType.MemberAccess;
        }

        public bool IsParameter()
        {
            return _expression.NodeType == ExpressionType.Parameter;
        }

        public bool IsConstant()
        {
            return _expression.NodeType == ExpressionType.Constant;
        }

        public bool IsMemberInit()
        {
            return _expression.NodeType == ExpressionType.MemberInit;
        }

        public bool IsNew()
        {
            return _expression.NodeType == ExpressionType.New;
        }

        public ConstantExpression GetConstant()
        {
            return _expression as ConstantExpression;
        }

        public object CompileAndInvoke(params object[] parameters)
        {
            return Expression.Lambda(_expression).Compile().DynamicInvoke(parameters);
        }

        public Delegate Compile()
        {
            return Expression.Lambda(_expression).Compile();
        }

        internal bool IsEnumerable()
        {
            if (!IsMethod())
                throw new ExpressionQualifiedException("Not a method expression");
            var methodExpression = GetMethodExpression();
            return methodExpression.Method.DeclaringType == typeof(Enumerable);
        }
    }
}
