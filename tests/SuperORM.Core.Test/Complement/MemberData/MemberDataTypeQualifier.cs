using SuperORM.Core.Utilities;
using System.Collections.Generic;

namespace SuperORM.Core.Test.Complement.MemberData
{
    public class MemberDataTypeQualifier
    {
        public static IEnumerable<object[]> TestCases
        {
            get
            {
                foreach (var a in GetExpressions())
                {
                    yield return a;
                }
            }
        }

        private static IEnumerable<object[]> GetExpressions()
        {
            object variable = true;
            TypeQualifier typeQualifier = new TypeQualifier(variable);

            yield return new object[] { typeQualifier.IsBoolean(), true };

        }
    }
}
