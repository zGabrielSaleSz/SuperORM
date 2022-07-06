using SuperORM.Core.Domain.Model.Enum;
using SuperORM.Core.Domain.Model.QueryBuilder;
using SuperORM.Core.Domain.Model.QueryBuilder.Fields;
using System.Collections.Generic;

namespace SuperORM.Core.Interface.QueryBuilder
{
    public interface ISelectableBuilder : IWhereable<ISelectableBuilder>, IQueryBuilder
    {
        ISelectableBuilder Select(IFieldsBuilder fieldsBuilder);
        ISelectableBuilder Select(IEnumerable<IField> fields);
        ISelectableBuilder Select(params IField[] fields);
        ISelectableBuilder Select(params string[] fields);
        ISelectableBuilder Top(uint rows);
        ISelectableBuilder Skip(uint rows);
        ISelectableBuilder Take(uint rows);
        ISelectableBuilder From(Table table);
        ISelectableBuilder From(string table);
        ISelectableBuilder InnerJoin(IField field, IField field2);
        ISelectableBuilder LeftJoin(IField field, IField field2);
        ISelectableBuilder RightJoin(IField field, IField field2);
        ISelectableBuilder CrossJoin(IField field, IField field2);
        ISelectableBuilder FullJoin(IField field, IField field2);
        ISelectableBuilder SelfJoin(IField field, IField field2);
        ISelectableBuilder GroupBy(params IField[] fields);
        ISelectableBuilder Having<T>(IField fields, SqlComparator sqlOperator, T value);
        ISelectableBuilder OrderBy(params IField[] fields);
        ISelectableBuilder OrderByDescending(params IField[] fields);
        ISelectableBuilder Limit(uint rows);
        ISelectableBuilder Limit(uint startIndex, uint amount);
        IQuerySintax GetQuerySintax();
        bool HasJoin();
        bool HasWhere();
        bool HasGroupBy();
        bool HasOrderBy();
        bool HasLimit();
        bool HasSelect();
        IEnumerable<IField> GetFields();
    }
}
