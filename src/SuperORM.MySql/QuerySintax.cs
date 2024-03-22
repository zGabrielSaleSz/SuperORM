using SuperORM.Core.Domain.Model.Evaluate.Interface;
using SuperORM.Core.Domain.Service;
using SuperORM.Core.Interface;
using System;

namespace SuperORM.MySql
{
    public class QuerySintax : IQuerySintax
    {
        public bool IsTopAvailable()
        {
            return false;
        }

        public string GetColumnAlias(string alias)
        {
            return $" AS `{alias}`";
        }

        public string GetColumnName(string column)
        {
            return $"`{column}`";
        }

        public string GetParameterKey(string parameterName)
        {
            return $"`{parameterName}`";
        }

        public string GetTableAlias(string alias)
        {
            return $"`{alias}`";
        }

        public string GetTableName(string tableName)
        {
            return $"`{tableName}`";
        }

        public string GetTableSchema(string schema)
        {
            return "";
        }

        public IEvaluateResult GetValue(long value)
        {
            return EvaluateResultFactory.AsSqlRaw(value.ToString(), value);
        }

        public IEvaluateResult GetValue(bool value)
        {
            if (value)
                return EvaluateResultFactory.AsSqlRaw("true", value);
            else
                return EvaluateResultFactory.AsSqlRaw("false", value);
        }

        public IEvaluateResult GetValue(string value)
        {
            return EvaluateResultFactory.AsValue(value.ToString(), value);
        }

        public IEvaluateResult GetValue(char value)
        {
            return EvaluateResultFactory.AsValue(value.ToString(), value);
        }

        public IEvaluateResult GetValue(byte value)
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

        public string GetPaginationSintax(uint rowsTake)
        {
            return $"LIMIT {rowsTake}";
        }

        public string GetPaginationSintax(uint rowsSkip, uint rowsTake)
        {
            return $"LIMIT {rowsSkip}, {rowsTake}";
        }

        public string GetInsertComplementRetrievePrimaryKey()
        {
            return "SELECT LAST_INSERT_ID();";
        }
    }
}
