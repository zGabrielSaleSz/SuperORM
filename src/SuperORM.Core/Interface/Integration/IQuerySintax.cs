using SuperORM.Core.Domain.Evaluate.Result;
using System;

namespace SuperORM.Core.Interface.Integration
{
    public interface IQuerySintax
    {
        bool IsTopAvailable();
        string GetTableSchema(string schema);
        string GetTableName(string tableName);
        string GetTableAlias(string alias);
        string GetColumnName(string column);
        string GetColumnAlias(string alias);
        string GetParameterKey(string parameterName);
        IEvaluateResult GetValue(long value);
        IEvaluateResult GetValue(bool value);
        IEvaluateResult GetValue(string value);
        IEvaluateResult GetValue(char value);
        IEvaluateResult GetValue(byte value);
        IEvaluateResult GetValue(DateTime value);
        string GetPaginationSintax(uint rowsTake);
        string GetPaginationSintax(uint rowsSkip, uint rowsTake);
        string GetInsertComplementRetrievePrimaryKey();
    }
}
