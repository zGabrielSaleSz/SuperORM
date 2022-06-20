namespace SuperORM.Core.Domain.Exceptions
{
    public class SqlDriverExpressionNotSupportedException : SuperOrmException
    {
        public SqlDriverExpressionNotSupportedException() : base()
        {

        }

        public SqlDriverExpressionNotSupportedException(string message) : base(message)
        {

        }
    }
}
