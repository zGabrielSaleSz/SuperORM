using SuperORM.Core.Domain.Evaluate.ColumnEvaluation;
using SuperORM.Core.Domain.Exceptions;
using SuperORM.Core.Domain.Model.LinqSQL;
using SuperORM.Core.Domain.Model.QueryBuilder;
using SuperORM.Core.Domain.Model.QueryBuilder.Fields;
using SuperORM.Core.Domain.Model.QueryBuilder.Fields.FieldsArgument;
using SuperORM.Core.Domain.Model.Sql;
using SuperORM.Core.Domain.Service.Evaluator;
using SuperORM.Core.Domain.Service.LinqSQL.SelectableTools;
using SuperORM.Core.Domain.Service.QueryBuilder;
using SuperORM.Core.Interface.Integration;
using SuperORM.Core.Interface.LinqSQL;
using SuperORM.Core.Interface.QueryBuilder.SqlOperations;
using SuperORM.Core.Interface.Repository;
using SuperORM.Core.Utilities.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SuperORM.Core.Domain.Service.LinqSQL
{
    public class Selectable<T> : ISelectable<T> where T : new()
    {
        private readonly TableAssimilator _tableAssimilator;
        private readonly IConnection _connection;
        private readonly ISelectableBuilder _selectableBuilder;
        private readonly IQuerySintax _querySintax;

        private ColumnAssimilator columnAssimilator = ColumnAssimilator.Empty;
        private IRepositoryRegistry repositoryRegistry;

        public Selectable(IConnection connection, IQuerySintax querySintax)
        {
            _connection = connection;
            _querySintax = querySintax;
            _selectableBuilder = new SelectableBuilder(querySintax);
            _tableAssimilator = new TableAssimilator(typeof(T));
            ConfigureMainTable();
        }

        private void ConfigureMainTable()
        {
            _selectableBuilder.From(_tableAssimilator.GetMainTable());
        }

        public ISelectable<T> AddColumnAssimilation(ColumnAssimilator columnAssimilation)
        {
            this.columnAssimilator = columnAssimilation;
            return this;
        }

        public ISelectable<T> SelectAll()
        {
            ReflectionHandler<T> reflectionHandler = new Utilities.Reflection.ReflectionHandler<T>(new T());
            foreach (string propertyName in reflectionHandler.GetPropertiesName())
            {
                IField field = GetFieldByType<T>(propertyName);
                _selectableBuilder.Select(field);
            }
            return this;
        }

        public ISelectable<T> Select(params Expression<Func<T, object>>[] expressions)
        {
            return Select<T>(expressions);
        }

        public ISelectable<T> Select<T2>(params Expression<Func<T2, object>>[] expressions)
        {
            foreach (var expression in expressions)
            {
                IField field = GetFieldByExpression(expression);
                _selectableBuilder.Select(field);
            }
            return this;
        }

        public ISelectable<T> Top(uint rows)
        {
            _selectableBuilder.Top(rows);
            return this;
        }

        public ISelectable<T> Skip(uint rows)
        {
            _selectableBuilder.Skip(rows);
            return this;
        }

        public ISelectable<T> Take(uint rows)
        {
            _selectableBuilder.Take(rows);
            return this;
        }

        public ISelectable<T> InnerJoin<T2>(string tableName, Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined, string alias = "")
            => InnerJoin<T, T2>(tableName, attributeRoot, attributeJoined, alias);

        public ISelectable<T> InnerJoin<TReference>(Expression<Func<T, object>> attributeRoot, Expression<Func<TReference, object>> attributeJoined, IJoinAdapter<TReference> reference)
            => InnerJoin<T, TReference>(GetTableOfType<TReference>(), attributeRoot, attributeJoined, reference.GetAlias());

        public ISelectable<T> InnerJoin<T1, T2>(string tableName, Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined, string alias = "")
        {
            var joinResult = GetJoinFields(tableName, attributeRoot, attributeJoined, alias);
            _selectableBuilder.InnerJoin(joinResult.FieldTable, joinResult.FieldTableJoined);
            return this;
        }

        public ISelectable<T> LeftJoin<T2>(string tableName, Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined)
            => LeftJoin<T, T2>(tableName, attributeRoot, attributeJoined);

        public ISelectable<T> LeftJoin<T1, T2>(string tableName, Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined)
        {
            var joinResult = GetJoinFields(tableName, attributeRoot, attributeJoined);
            _selectableBuilder.LeftJoin(joinResult.FieldTable, joinResult.FieldTableJoined);
            return this;
        }

        public ISelectable<T> RightJoin<T2>(string tableName, Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined)
            => RightJoin<T, T2>(tableName, attributeRoot, attributeJoined);

        public ISelectable<T> RightJoin<T1, T2>(string tableName, Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined)
        {
            var joinResult = GetJoinFields(tableName, attributeRoot, attributeJoined);
            _selectableBuilder.RightJoin(joinResult.FieldTable, joinResult.FieldTableJoined);
            return this;
        }

        public ISelectable<T> CrossJoin<T2>(string tableName, Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined)
            => CrossJoin<T, T2>(tableName, attributeRoot, attributeJoined);

        public ISelectable<T> CrossJoin<T1, T2>(string tableName, Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined)
        {
            var joinResult = GetJoinFields(tableName, attributeRoot, attributeJoined);
            _selectableBuilder.CrossJoin(joinResult.FieldTable, joinResult.FieldTableJoined);
            return this;
        }

        public ISelectable<T> FullJoin<T2>(string tableName, Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined)
            => FullJoin<T, T2>(tableName, attributeRoot, attributeJoined);

        public ISelectable<T> FullJoin<T1, T2>(string tableName, Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined)
        {
            var joinResult = GetJoinFields(tableName, attributeRoot, attributeJoined);
            _selectableBuilder.FullJoin(joinResult.FieldTable, joinResult.FieldTableJoined);
            return this;
        }

        public ISelectable<T> SelfJoin<T2>(string tableName, Expression<Func<T, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined)
            => SelfJoin<T, T2>(tableName, attributeRoot, attributeJoined);

        public ISelectable<T> SelfJoin<T1, T2>(string tableName, Expression<Func<T1, object>> attributeRoot, Expression<Func<T2, object>> attributeJoined)
        {
            var joinResult = GetJoinFields(tableName, attributeRoot, attributeJoined);
            _selectableBuilder.SelfJoin(joinResult.FieldTable, joinResult.FieldTableJoined);
            return this;
        }

        private JoinEvaluated GetJoinFields<T1, T2>(string tableName, Expression<Func<T1, object>> expression, Expression<Func<T2, object>> expression1, string alias = "")
        {
            JoinEvaluated result = new JoinEvaluated();
            string tableColumn = new SqlExpressionEvaluator(expression.Body, _querySintax).Evaluate();
            string joinedTableColumn = new SqlExpressionEvaluator(expression1.Body, _querySintax).Evaluate();

            Table tableOne = GetTableByType(typeof(T1));
            Table tableTwo = GetTableReferenceOf(typeof(T2), tableName);
            if (!string.IsNullOrWhiteSpace(alias))
                tableTwo.SetAlias(alias);
            result.FieldTable = GetFieldReferenceOf(tableOne, tableColumn);
            result.FieldTableJoined = GetFieldReferenceOf(tableTwo, joinedTableColumn);
            return result;
        }

        public IEnumerable<T> AsEnumerable()
        {
            ParameterizedQuery parameterizedQuery = GetQueryWithParameters();

            CommandReaderContext readerContext = new CommandReaderContext(parameterizedQuery);
            foreach (Dictionary<string, object> columnsAndValues in _connection.ExecuteReader(readerContext))
            {
                yield return EntityBuilder.Build<T>(columnsAndValues, columnAssimilator);
            }
        }

        public IEnumerable<ResultPicker> GetResult()
        {
            MultipleFieldAssimilator multipleFieldAssimilator = new MultipleFieldAssimilator(_selectableBuilder.GetFields());
            multipleFieldAssimilator.UpdateUniqueAlias();

            ParameterizedQuery parameterizedQuery = GetQueryWithParameters();
            CommandReaderContext readerContext = new CommandReaderContext(parameterizedQuery);

            foreach (Dictionary<string, object> columnsAndValues in _connection.ExecuteReader(readerContext))
            {
                ResultPickerHeader resultPickerHeader = BuildResultPickerHeader(multipleFieldAssimilator, columnsAndValues);
                ResultPicker resultPicker = new ResultPicker(resultPickerHeader);
                yield return resultPicker;
            }
        }

        private ResultPickerHeader BuildResultPickerHeader(MultipleFieldAssimilator multipleFieldAssimilator, Dictionary<string, object> columnsAndValues)
        {
            ResultPickerHeader resultPickerHeader = new ResultPickerHeader(columnAssimilator);
            foreach (var columnAndValue in columnsAndValues)
            {
                IField field = multipleFieldAssimilator.GetFieldByAlias(columnAndValue.Key);
                Table table = field.GetTable();
                Type type = _tableAssimilator.GetTypeByTable(table);
                resultPickerHeader.Add(type, field.GetFieldName(), columnAndValue.Value);
            }
            return resultPickerHeader;
        }

        public T FirstOrDefault()
        {
            if (_selectableBuilder.HasOrderBy() && !_selectableBuilder.HasLimit())
            {
                _selectableBuilder.Limit(1);
            }
            return AsEnumerable().ToArray().FirstOrDefault();
        }

        public ISelectable<T> From(string tableName)
        {
            _tableAssimilator.SetMainTableName(tableName);
            _selectableBuilder.From(_tableAssimilator.GetMainTable());
            return this;
        }

        public ISelectable<T> From<T2>()
            => From(GetTableOfType<T2>());


        public ISelectable<T> Select(string field)
        {
            _selectableBuilder.Select(field);
            return this;
        }

        public ISelectable<T> Where(Expression<Func<T, bool>> expression)
        {
            IEvaluateColumn evaluateColumn = new EvaluateColumnQueryBuilder<T>(_tableAssimilator, _querySintax, columnAssimilator);
            _selectableBuilder.SetWhereCondition(expression, evaluateColumn);
            return this;
        }

        public ISelectable<T> GroupBy(params Expression<Func<T, object>>[] attributes)
        {
            GroupBy<T>(attributes);
            return this;
        }

        public ISelectable<T> GroupBy<T1>(params Expression<Func<T1, object>>[] attributes)
        {
            foreach (var attribute in attributes)
            {
                _selectableBuilder.GroupBy(GetFieldByExpression(attribute));
            }
            return this;
        }
        public ISelectable<T> OrderBy(params Expression<Func<T, object>>[] attributes)
        {
            OrderBy<T>(attributes);
            return this;
        }

        public ISelectable<T> OrderBy<T1>(params Expression<Func<T1, object>>[] attributes)
        {
            foreach (var attribute in attributes)
            {
                _selectableBuilder.OrderBy(GetFieldByExpression(attribute));
            }
            return this;
        }

        public ISelectable<T> OrderByDescending(params Expression<Func<T, object>>[] attributes)
        {
            OrderByDescending<T>(attributes);
            return this;
        }

        public ISelectable<T> OrderByDescending<T1>(params Expression<Func<T1, object>>[] attributes)
        {
            foreach (var attribute in attributes)
            {
                _selectableBuilder.OrderByDescending(GetFieldByExpression(attribute));
            }
            return this;
        }

        public ISelectable<T> Limit(uint rows)
        {
            _selectableBuilder.Limit(rows);
            return this;
        }

        public ISelectable<T> Limit(uint startIndex, uint amount)
        {
            _selectableBuilder.Limit(startIndex, amount);
            return this;
        }

        public ISelectable<T> UseRepositoryRegistry(IRepositoryRegistry repositoryRegistry)
        {
            this.repositoryRegistry = repositoryRegistry;
            return this;
        }

        public string GetQuery()
        {
            CheckRequired();
            FillEmptySelect();
            return _selectableBuilder.GetQuery();
        }

        public ParameterizedQuery GetQueryWithParameters()
        {
            CheckRequired();
            FillEmptySelect();
            return _selectableBuilder.GetQueryWithParameters();
        }

        private void CheckRequired()
        {
            CheckRequiredMainTable();
        }

        private void FillEmptySelect()
        {
            if (!_selectableBuilder.HasSelect())
                SelectAll();
        }

        private void CheckRequiredMainTable()
        {
            if (!_tableAssimilator.MainTableHasName())
                throw new SelectableIncompleteException("Use From method to setup the main table name");
        }

        private Table GetTableReferenceOf(Type type, string name = "")
        {
            if (_tableAssimilator.TableHasReferences(type))
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    _tableAssimilator.SetTableName(type, name);
                }
                return _tableAssimilator.GetTableReference(type);
            }
            else
            {
                return _tableAssimilator.AddTableReference(type, name);
            }
        }

        private Table GetTableByType(Type type)
        {
            return _tableAssimilator.GetTableReference(type);
        }

        private IField GetFieldReferenceOf(Table table, string column)
        {
            IField field = table.GetFieldsBuilder().Find(column);
            if (field == null)
            {
                Type type = _tableAssimilator.GetTypeByTable(table);
                string respectiveName = columnAssimilator.GetByProperty(type, column);
                field = table.AddField<Column>(respectiveName);
            }
            return field;
        }

        private IField GetFieldByExpression<T2>(Expression<Func<T2, object>> expression)
        {
            SqlExpressionEvaluator sqlEvaluator = new SqlExpressionEvaluator(expression.Body, _querySintax);
            Table table = GetTableReferenceOf(typeof(T2));
            IField field = GetFieldReferenceOf(table, sqlEvaluator.Evaluate());
            return field;
        }

        private IField GetFieldByType<T2>(string propertyName)
        {
            Table table = GetTableReferenceOf(typeof(T2));
            IField field = GetFieldReferenceOf(table, propertyName);
            return field;
        }

        public string GetTableOfType<Type>()
        {
            if (repositoryRegistry == null)
                throw new NotFoundedRepositoryRegistryException();
            return repositoryRegistry.GetRepository(typeof(Type)).GetTableName();
        }
    }
}
