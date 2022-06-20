namespace SuperORM.Core.Domain.Model.QueryBuilder.Fields.FieldsArgument
{
    public class Count : IFunctionArgument
    {
        public Count() { }
        public string Handle(string columnReference)
        {
            return $"COUNT({columnReference})";
        }
    }
}
