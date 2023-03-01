using SuperORM.Core.Domain.Evaluate.Context;
using SuperORM.Core.Domain.Evaluate.QuerySintax;
using SuperORM.Core.Domain.Model.QueryBuilder.Parameters;
using SuperORM.Core.Interface.Integration;
using System.Linq.Expressions;

namespace SuperORM.Core.Domain.Service.Evaluator
{
    public class SqlExpressionEvaluator
    {
        private readonly Expression MainExpression;
        private readonly IQuerySintax QuerySintax;
        public EvaluateContext EvaluateContext { get; private set; }
        public SqlExpressionEvaluator(Expression expression, IQuerySintax querySintax = null)
        {
            MainExpression = expression;

            if (querySintax == null)
                QuerySintax = new QuerySintaxDefault();

            else
                QuerySintax = querySintax;

            EvaluateContext = new EvaluateContext(QuerySintax, MainExpression);
        }

        public string Evaluate()
        {
            string evaluateResult = EvaluateWithParameters();
            ParameterReplacer parameterReplacer = new ParameterReplacer(QuerySintax, evaluateResult, EvaluateContext.GetParametersBuilder());
            return parameterReplacer.GetResult();
        }

        public string EvaluateWithParameters()
        {
            string evaluatedWithParameters = ExpressionEvaluatorStrategy.Evaluate(EvaluateContext).GetValue();
            return evaluatedWithParameters;
        }
    }
}
