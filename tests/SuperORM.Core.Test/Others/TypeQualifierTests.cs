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
            Assert.Equal(dateTimeEvaluator.GetAs<DateTime>(), new DateTime(1990, 08, 10));
        }
    }
}
