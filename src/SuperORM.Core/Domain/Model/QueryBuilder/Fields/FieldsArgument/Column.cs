namespace SuperORM.Core.Domain.Model.QueryBuilder.Fields.FieldsArgument
{
    public class Column : IFieldArgument
    {
        public string Handle(string columnReference)
        {
            return columnReference;
        }
    }
}
