namespace SuperORM.Core.Domain.Exceptions
{
    public class InvalidNonNullableTypeException : SuperOrmException
    {
        public InvalidNonNullableTypeException(string message) : base(message)
        {

        }
    }
}
