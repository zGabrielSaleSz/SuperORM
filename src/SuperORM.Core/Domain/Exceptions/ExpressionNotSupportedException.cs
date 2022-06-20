namespace SuperORM.Core.Domain.Exceptions
{
    public class ExpressionNotSupportedException : SuperOrmException
    {
        public ExpressionNotSupportedException()
        {

        }
        public ExpressionNotSupportedException(string message) : base(message)
        {

        }
    }
}
