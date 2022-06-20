using SuperORM.Core.Domain.Model.QueryBuilder.Parameters;
using SuperORM.Core.Interface;
using SuperORM.Core.Utilities;
using System.Linq.Expressions;

namespace SuperORM.Core.Domain.Model.Evaluate.Interface
{
    public interface IEvaluateContext
    {
        IEvaluateContext BuildChild<T>(T expressionChild) where T : Expression;
        IEvaluateContext GetParent();
        ExpressionQualifier GetQualifier();
        IQuerySintax GetQuerySintax();
        ParametersBuilder GetParametersBuilder();
        void SetParametersBuilder(ParametersBuilder parametersBuilder);
        IEvaluateColumn GetEvaluateColumn();
        void SetColumnEvaluator(IEvaluateColumn evaluateColumn);
        bool IsRoot();
    }
}
