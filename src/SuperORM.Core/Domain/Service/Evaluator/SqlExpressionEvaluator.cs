using SuperORM.Core.Domain.Model.Evaluate;
using SuperORM.Core.Domain.Model.QueryBuilder.Parameters;
using SuperORM.Core.Domain.Service.Evaluator;
using SuperORM.Core.Interface;
using System.Linq.Expressions;

namespace SuperORM.Core.Domain.Service
{
    public class SqlExpressionEvaluator
    {
        private readonly Expression MainExpression;
        private readonly IQuerySintax QuerySintax;
        public EvaluateContext EvaluateContext { get; private set; }
        public SqlExpressionEvaluator(Expression expression, IQuerySintax querySintax = null)
        {
            this.MainExpression = expression;

            if (querySintax == null)
                QuerySintax = new QuerySintaxDefault();

            else
                this.QuerySintax = querySintax;

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
