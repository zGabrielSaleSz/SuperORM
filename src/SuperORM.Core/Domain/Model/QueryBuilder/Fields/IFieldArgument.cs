namespace SuperORM.Core.Domain.Model.QueryBuilder.Fields
{
    public interface IFieldArgument
    {
        string Handle(string columnReference);
    }
}
