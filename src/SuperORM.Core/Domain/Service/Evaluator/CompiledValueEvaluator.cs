using SuperORM.Core.Domain.Evaluate.Result;
using SuperORM.Core.Domain.Evaluate.Result.Factory;
using SuperORM.Core.Interface.Integration;
using SuperORM.Core.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SuperORM.Core.Domain.Service.Evaluator
{
    public class CompiledValueEvaluator
    {
        public static IEvaluateResult Evaluate(IQuerySintax querySintax, object compiledResult)
        {
            TypeQualifier typeQualifier = new TypeQualifier(compiledResult);
            if (typeQualifier.IsNull())
                return EvaluateResultFactory.AsNull();

            if (typeQualifier.IsTypeOf<bool>())
                return querySintax.GetValue(typeQualifier.GetAs<bool>());

            if (typeQualifier.IsTypeOf<byte>())
                return querySintax.GetValue(typeQualifier.GetAs<byte>());

            if (typeQualifier.IsTypeOf<string>())
                return querySintax.GetValue(typeQualifier.GetAs<string>());

            if (typeQualifier.IsTypeOf<long>() || typeQualifier.IsTypeOf<int>() || typeQualifier.IsTypeOf<short>())
                return querySintax.GetValue(typeQualifier.GetAs<long>());

            if (typeQualifier.IsTypeOf<char>())
                return querySintax.GetValue(typeQualifier.GetAs<char>());

            if (typeQualifier.IsTypeOf<DateTime>())
                return querySintax.GetValue(typeQualifier.GetAs<DateTime>());

            if (typeQualifier.IsEnumerable())
                return HandleEnumerable(querySintax, typeQualifier.GetAsEnumerable());

            Type unsupportedType = typeQualifier.GetVariableType();
            throw new ArgumentException("Unsupported type!", unsupportedType.FullName);
        }

        private static IEvaluateResult HandleEnumerable(IQuerySintax querySintax, IEnumerable arrayObject)
        {
            List<IEvaluateResult> results = new List<IEvaluateResult>();
            foreach (object value in arrayObject.Cast<object>())
            {
                results.Add(Evaluate(querySintax, value));
            }
            string result = string.Join(",", results.Select(r => r.GetValue()));
            return EvaluateResultFactory.AsSqlRaw(result, arrayObject);
        }
    }
}
