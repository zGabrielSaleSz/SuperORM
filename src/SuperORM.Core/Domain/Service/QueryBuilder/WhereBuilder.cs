using SuperORM.Core.Domain.Evaluate.Result;
using SuperORM.Core.Domain.Model.Enum;
using SuperORM.Core.Domain.Model.QueryBuilder.Fields;
using SuperORM.Core.Domain.Model.QueryBuilder.Parameters;
using SuperORM.Core.Domain.Service.Adapters;
using SuperORM.Core.Domain.Service.Evaluator;
using SuperORM.Core.Interface.Integration;

namespace SuperORM.Core.Domain.Service.QueryBuilder
{
    public class WhereBuilder
    {
        private readonly IQuerySintax _querySintax;
        private readonly ParametersBuilder _parametersBuilder;
        public WhereBuilder(IQuerySintax querySintax, ParametersBuilder parametersBuilder)
        {
            _querySintax = querySintax;
            _parametersBuilder = parametersBuilder;
        }

        public string Build<T>(IField field, SqlComparator sqlOperator, T value)
        {
            string column = field.GetFieldValue(_querySintax);
            string comparator = GetComparatorBasedInValue(sqlOperator, value);
            IEvaluateResult evaluateResult = CompiledValueEvaluator.Evaluate(_querySintax, value);
            IEvaluateParameter parameter = _parametersBuilder.Add(evaluateResult, "whereBuilder");
            return $"{column} {comparator} {parameter.GetValue()}";
        }

        public string Build(IField field, SqlComparator sqlOperator, IField field2)
        {
            string column = field.GetFieldValue(_querySintax);
            string comparator = GetUsualComparator(sqlOperator);
            string valueString = field2.GetFieldValue(_querySintax);
            return $"{column} {comparator} {valueString}";
        }

        private static string GetUsualComparator(SqlComparator sqlOperator)
        {
            return SqlHandler.GetByNodeType(ExpressionTypeAdapter.GetExpressionType(sqlOperator));
        }

        private static string GetComparatorBasedInValue<T>(SqlComparator sqlOperator, T value)
        {
            return SqlHandler.GetComparator(ExpressionTypeAdapter.GetExpressionType(sqlOperator), value);
        }
    }
}
