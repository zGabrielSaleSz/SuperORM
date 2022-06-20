namespace SuperORM.Core.Domain.Model.QueryBuilder.Fields.FieldsArgument
{
    public class Min : IFunctionArgument
    {
        public string Handle(string columnReference)
        {
            return $"MIN({columnReference})";
        }
    }
}
