using SuperORM.Core.Domain.Model.Evaluate.Interface;
using System;

namespace SuperORM.Core.Interface
{
    public interface IQuerySintax
    {
        public bool IsTopAvailable();
        public string GetTableSchema(string schema);
        public string GetTableName(string tableName);
        public string GetTableAlias(string alias);
        public string GetColumnName(string column);
        public string GetColumnAlias(string alias);
        public string GetParameterKey(string parameterName);
        public IEvaluateResult GetValue(long value);
        public IEvaluateResult GetValue(bool value);
        public IEvaluateResult GetValue(string value);
        public IEvaluateResult GetValue(char value);
        public IEvaluateResult GetValue(byte value);
        public IEvaluateResult GetValue(DateTime value);
        public string GetPaginationSintax(uint rowsTake);
        public string GetPaginationSintax(uint rowsSkip, uint rowsTake);
        string GetInsertComplementRetrievePrimaryKey();
    }
}
