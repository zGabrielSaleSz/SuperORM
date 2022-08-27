using SuperORM.Core.Domain.Model.Evaluate.Interface;
using SuperORM.Core.Domain.Service;
using SuperORM.Core.Interface;
using System;

namespace SuperORM.Core.Domain.Model.Evaluate
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
            throw new System.NotImplementedException();
        }

        public string GetPaginationSintax(uint rowsSkip, uint rowsTake)
        {
            throw new System.NotImplementedException();
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

        public IEvaluateResult GetValue(decimal value)
        {
            return EvaluateResultFactory.AsValue(value.ToString(), value);
        }

        public IEvaluateResult GetValue(double value)
        {
            return EvaluateResultFactory.AsValue(value.ToString(), value);
        }

        public IEvaluateResult GetValue(float value)
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
