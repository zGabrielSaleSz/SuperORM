namespace SuperORM.Core.Domain.Model.QueryBuilder.Fields.FieldsArgument
{
    public class Max : IFunctionArgument
    {
        public Max() { }
        public string Handle(string columnReference)
        {
            return $"MAX({columnReference})";
        }
    }
}
