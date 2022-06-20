using SuperORM.Core.Domain.Service;
using SuperORM.Core.Test.Complement.Model;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace SuperORM.Core.Test.Others
{
    public class SqlEvaluatorTests
    {
        [Theory]
        [MemberData(nameof(Complement.MemberData.MemberDataSqlEvaluator.TestCases), MemberType = typeof(Complement.MemberData.MemberDataSqlEvaluator))]
        public void Should_ReturnWhereConditionString_When_ExpressionIsProvided(Expression<Func<User, bool>> expression, string queryResult)
        {
            SqlExpressionEvaluator sqlEvaluator = new SqlExpressionEvaluator(expression.Body);
            string value = sqlEvaluator.Evaluate();

            Assert.Equal(queryResult, value);
        }

        [Fact]
        public void Should_ReturnSqlConditionString_When_UsingOperations()
        {
            string expectedReverted = "((id * 3) = ((1 + 4) - 0))";
            int variavel = 4;
            Expression<Func<User, bool>> expression = expression = u => u.id * 3 == (1 + variavel - (4 / 2) % 1);

            SqlExpressionEvaluator builderReverted = new SqlExpressionEvaluator(expression.Body);
            string result = builderReverted.Evaluate();
            Assert.Equal(expectedReverted, result);
        }

        [Fact]
        public void Should_ReturnLike_When_UsingContainsAsString()
        {
            string expected = "(email LIKE '%gabriel.s479%')";
            Expression<Func<User, bool>> expression = u => u.email.Contains("gabriel.s479");

            SqlExpressionEvaluator builder = new SqlExpressionEvaluator(expression.Body);
            string evaluationResult = builder.Evaluate();

            Assert.Equal(expected, evaluationResult);
        }

        [Fact]
        public void Should_ReturnIn_When_UsingContainsAsInteger()
        {
            int[] validIds = new int[] { 1, 2, 56 };
            string expected = "(id IN (1,2,56))";
            Expression<Func<User, bool>> expression = u => validIds.Contains(u.id);

            SqlExpressionEvaluator builder = new SqlExpressionEvaluator(expression.Body);
            Assert.Equal(expected, builder.Evaluate());
        }

        [Fact]
        public void Should_ReturnNotIn_When_UsingContainsAsInteger()
        {
            int[] validIds = new int[] { 1, 2, 56 };
            string expectedReverted = "NOT (id IN (1,2,56))";
            Expression<Func<User, bool>> expression = expression = u => !validIds.Contains(u.id);

            SqlExpressionEvaluator builderReverted = new SqlExpressionEvaluator(expression.Body);
            string result = builderReverted.Evaluate();
            Assert.Equal(expectedReverted, result);
        }

        [Fact]
        public void Should_ReturnNotEqual_When_UsingNotAsUnaryExpression()
        {
            string expectedReverted = "NOT (id = 0)";
            Expression<Func<User, bool>> expression = expression = u => !(u.id == 0);

            SqlExpressionEvaluator builderReverted = new SqlExpressionEvaluator(expression.Body);
            string result = builderReverted.Evaluate();
            Assert.Equal(expectedReverted, result);
        }

        [Fact]
        public void Should_ReturnNot_When_UsingFalseOperator()
        {
            string expectedReverted = "NOT active";
            Expression<Func<User, bool>> expression = expression = u => !u.active;

            SqlExpressionEvaluator builderReverted = new SqlExpressionEvaluator(expression.Body);
            string result = builderReverted.Evaluate();
            Assert.Equal(expectedReverted, result);
        }

        [Fact]
        public void Should_ReturnField_When_UsingTrueOperator()
        {
            string expectedReverted = "active";
            Expression<Func<User, bool>> expression = expression = u => u.active;

            SqlExpressionEvaluator builderReverted = new SqlExpressionEvaluator(expression.Body);
            string result = builderReverted.Evaluate();
            Assert.Equal(expectedReverted, result);
        }

        [Fact]
        public void Should_ThrowException_When_UnsupportedMethodIsUsed()
        {
            // Arrange
            Expression<Func<User, bool>> expression = u => u.email.All(e => e == 'c');
            SqlExpressionEvaluator builder = new SqlExpressionEvaluator(expression.Body);

            // Act and Assert
            Assert.Throws<Exception>(() => builder.Evaluate());
        }
    }
}
