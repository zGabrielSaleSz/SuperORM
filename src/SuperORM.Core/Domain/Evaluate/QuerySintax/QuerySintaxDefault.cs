using SuperORM.Core.Domain.Evaluate.Result;
using SuperORM.Core.Domain.Evaluate.Result.Factory;
using SuperORM.Core.Interface;
using System;

namespace SuperORM.Core.Domain.Evaluate.QuerySintax
{
    public class QuerySintaxDefault : IQuerySintax
    {
        public string GetColumnAlias(string alias)
        {
            return $" AS {alias}";
        }

        public string GetColumnName(string column)
        {
            return column;
        }

        public string GetInsertComplementRetrievePrimaryKey()
        {
            return string.Empty;
        }

        public string GetPaginationSintax(uint rowsTake)
        {
            throw new NotImplementedException();
        }

        public string GetPaginationSintax(uint rowsSkip, uint rowsTake)
        {
            throw new NotImplementedException();
        }

        public string GetParameterKey(string parameterName)
        {
            return parameterName;
        }

        public string GetTableAlias(string alias)
        {
            return alias;
        }

        public string GetTableName(string tableName)
        {
            return tableName;
        }

        public string GetTableSchema(string schema)
        {
            return "";
        }

        public IEvaluateResult GetValue(object value)
        {
            return EvaluateResultFactory.AsValue(value.ToString(), value);
        }

        public IEvaluateResult GetValue(long value)
        {
            return EvaluateResultFactory.AsSqlRaw(value.ToString(), value);
        }

        public IEvaluateResult GetValue(bool value)
        {
            string option;
            if (value == true)
                option = "1";
            else
                option = "0";
            return EvaluateResultFactory.AsValue(option, value);
        }

        public IEvaluateResult GetValue(string value)
        {
            return EvaluateResultFactory.AsValue(value, value);
        }

        public IEvaluateResult GetValue(byte value)
        {
            return EvaluateResultFactory.AsValue(value.ToString(), value);
        }

        public IEvaluateResult GetValue(char value)
        {
            return EvaluateResultFactory.AsValue(value.ToString(), value);
        }

        public IEvaluateResult GetValue(DateTime value)
        {
            return EvaluateResultFactory.AsValue(value.ToString("yyyy-MM-dd HH:mm:ss"), value);
        }

        public bool IsTopAvailable()
        {
            return false;
        }
    }
}
