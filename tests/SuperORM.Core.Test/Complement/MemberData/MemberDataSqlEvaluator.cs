using SuperORM.Core.Test.Complement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SuperORM.Core.Test.Complement.MemberData
{
    internal class Test
    {
        internal Test()
        {
            arrayInt = new int[] { 1, 2, 3 };
        }
        public int[] arrayInt { get; set; }
    }
    public class MemberDataSqlEvaluator
    {
        public static IEnumerable<object[]> TestCases
        {
            get
            {
                foreach (object[] currentExpression in GetExpressions())
                {
                    yield return currentExpression;
                }
            }
        }

        private static IEnumerable<object[]> GetExpressions()
        {
            Test test = new Test();
            string myName = "gabriel";
            Expression<Func<User, bool>> expression;

            expression = u => u.email == "gabriel.s479@hotmail.com";
            yield return new object[] { expression, "(email = 'gabriel.s479@hotmail.com')" };

            expression = u => u.email.Contains('c');
            yield return new object[] { expression, "(email LIKE '%c%')" };

            expression = u => u.email.Contains('d');
            yield return new object[] { expression, "(email LIKE '%d%')" };

            expression = u => (u.id == 1 || u.email == "gabriel.sales@hotmail.com" || u.Name.Contains("João")) && u.password != null;
            yield return new object[] { expression, "((((id = 1) OR (email = 'gabriel.sales@hotmail.com')) OR (Name LIKE '%João%')) AND (password IS NOT NULL))" };

            expression = u => (u.id == 1 || u.email == "gabriel.sales@hotmail.com" || u.Name.Contains("John")) && u.password == null;
            yield return new object[] { expression, "((((id = 1) OR (email = 'gabriel.sales@hotmail.com')) OR (Name LIKE '%John%')) AND (password IS NULL))" };

            expression = u => u.password == "cryptographed one" && u.email.Contains("gabriel") && u.Name == myName && u.active == true;
            yield return new object[] { expression, "((((password = 'cryptographed one') AND (email LIKE '%gabriel%')) AND (Name = 'gabriel')) AND (active = '1'))" };

            expression = u => test.arrayInt.Contains(u.id);
            yield return new object[] { expression, "(id IN (1,2,3))" };
        }
    }
}
