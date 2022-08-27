using SuperORM.Core.Utilities;
using System;
using Xunit;

namespace SuperORM.Core.Test.Others
{
    public class TypeQualifierTests
    {
        //[Theory]
        //[MemberData(nameof(MemberData.MemberDataTypeQualifier.TestCases), MemberType = typeof(MemberData.MemberDataTypeQualifier))]
        //public void Should_ReturnWhereConditionString_When_ExpressionIsProvided(bool result, bool expected)
        //{
        //    Assert.Equal(expected, result);
        //}
        [Fact]
        public void Should_ReturnTrueAndConvert_When_VariableIsRespectiveType()
        {
            bool booleanTest = true;
            TypeQualifier booleanEvaluator = new TypeQualifier(booleanTest);

            Assert.True(booleanEvaluator.IsTypeOf<bool>());
            Assert.True(booleanEvaluator.GetAs<bool>());

            byte byteTest = 1;
            TypeQualifier byteEvaluator = new TypeQualifier(byteTest);
            Assert.True(byteEvaluator.IsTypeOf<byte>());
            Assert.Equal(1, byteEvaluator.GetAs<byte>());

            string stringTest = "should be that";
            TypeQualifier stringEvaluator = new TypeQualifier(stringTest);
            Assert.True(stringEvaluator.IsTypeOf<string>());
            Assert.Equal("should be that", stringEvaluator.GetAs<string>());

            DateTime dateTime = new DateTime(1990, 08, 10);
            TypeQualifier dateTimeEvaluator = new TypeQualifier(dateTime);
            Assert.True(dateTimeEvaluator.IsTypeOf<DateTime>());
            Assert.Equal(new DateTime(1990, 08, 10), dateTimeEvaluator.GetAs<DateTime>());

            short shortTest = 2;
            TypeQualifier shortEvaluator = new TypeQualifier(shortTest);
            Assert.True(shortEvaluator.IsTypeOf<short>());
            Assert.Equal(2, shortEvaluator.GetAs<short>());

            double doubleTest = 12.123D;
            TypeQualifier doubleEvaluator = new TypeQualifier(doubleTest);
            Assert.False(doubleEvaluator.IsNull());
            Assert.True(doubleEvaluator.IsTypeOf<double>());
            Assert.False(doubleEvaluator.IsTypeOf<double?>());
            Assert.NotNull(doubleEvaluator.GetAs<double?>());
            Assert.Equal(12.123D, doubleEvaluator.GetAs<double?>());

            double? nullableDoubleTest = null;
            TypeQualifier nullableDoubleEvaluator = new TypeQualifier(nullableDoubleTest);
            Assert.True(nullableDoubleEvaluator.IsNull());
            Assert.False(nullableDoubleEvaluator.IsTypeOf<double>());
            Assert.True(nullableDoubleEvaluator.IsTypeOf<double?>());
            Assert.Null(nullableDoubleEvaluator.GetAs<double?>());

            float floatTest = 12.125F;
            TypeQualifier floatEvaluator = new TypeQualifier(floatTest);
            Assert.False(floatEvaluator.IsNull());
            Assert.True(floatEvaluator.IsTypeOf<float>());
            Assert.False(floatEvaluator.IsTypeOf<float?>());
            Assert.NotNull(floatEvaluator.GetAs<float?>());
            Assert.Equal(12.125F, floatEvaluator.GetAs<float?>());

            float? nullableFloatTest = null;
            TypeQualifier nullableFloatEvaluator = new TypeQualifier(nullableFloatTest);
            Assert.True(nullableFloatEvaluator.IsNull());
            Assert.False(nullableFloatEvaluator.IsTypeOf<float>());
            Assert.True(nullableFloatEvaluator.IsTypeOf<float?>());
            Assert.Null(nullableFloatEvaluator.GetAs<float?>());

            decimal decimalTest = 12.122M;
            TypeQualifier decimalEvaluator = new TypeQualifier(decimalTest);
            Assert.False(decimalEvaluator.IsNull());
            Assert.True(decimalEvaluator.IsTypeOf<decimal>());
            Assert.False(decimalEvaluator.IsTypeOf<decimal?>());
            Assert.NotNull(decimalEvaluator.GetAs<decimal?>());
            Assert.Equal(12.122M, decimalEvaluator.GetAs<decimal?>());

            decimal? nullableDecimalTest = null;
            TypeQualifier nullableDecimalEvaluator = new TypeQualifier(nullableDecimalTest);
            Assert.True(nullableDecimalEvaluator.IsNull());
            Assert.False(nullableDecimalEvaluator.IsTypeOf<decimal>());
            Assert.True(nullableDecimalEvaluator.IsTypeOf<decimal?>());
            Assert.Null(nullableDecimalEvaluator.GetAs<decimal?>());
        }
    }
}
