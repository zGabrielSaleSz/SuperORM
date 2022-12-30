using SuperORM.Core.Domain.Evaluate.ColumnColumnEvaluation;
using SuperORM.Core.Domain.Evaluate.ColumnEvaluation;
using SuperORM.Core.Domain.Model.QueryBuilder.Parameters;
using SuperORM.Core.Interface;
using SuperORM.Core.Utilities;
using System.Linq.Expressions;

namespace SuperORM.Core.Domain.Evaluate.Context
{
    public class EvaluateContext : IEvaluateContext
    {
        private IEvaluateColumn _columnEvaluator;
        private ParametersBuilder _parametersBuilder;

        private readonly IQuerySintax _querySintax;
        private readonly Expression _expression;
        private readonly IEvaluateContext _parent;

        public EvaluateContext(IQuerySintax querySintax, Expression expression, ParametersBuilder parametersBuilder = null, IEvaluateContext parent = null, IEvaluateColumn columnEvaluator = null)
        {
            _querySintax = querySintax;
            _expression = expression;
            _parent = parent;

            if (columnEvaluator == null)
                _columnEvaluator = new EvaluateColumnDefault();
            else
                _columnEvaluator = columnEvaluator;

            if (parametersBuilder == null)
                _parametersBuilder = new ParametersBuilder();
            else
                _parametersBuilder = parametersBuilder;
        }

        public void SetColumnEvaluator(IEvaluateColumn evaluateColumn)
        {
            _columnEvaluator = evaluateColumn;
        }

        public void SetParametersBuilder(ParametersBuilder parametersBuilder)
        {
            _parametersBuilder = parametersBuilder;
        }

        public bool IsRoot()
        {
            return _parent == null;
        }

        public IEvaluateContext BuildChild<T>(T expressionChild) where T : Expression
        {
            return new EvaluateContext(_querySintax, expressionChild, _parametersBuilder, this, _columnEvaluator);
        }

        public IEvaluateContext GetParent()
        {
            return _parent;
        }

        public ExpressionQualifier GetQualifier()
        {
            return ExpressionQualifier.Of(_expression);
        }

        public IQuerySintax GetQuerySintax()
        {
            return _querySintax;
        }

        public IEvaluateColumn GetEvaluateColumn()
        {
            return _columnEvaluator;
        }

        public ParametersBuilder GetParametersBuilder()
        {
            return _parametersBuilder;
        }
    }
}
