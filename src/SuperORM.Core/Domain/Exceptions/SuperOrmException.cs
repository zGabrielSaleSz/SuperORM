using System;

namespace SuperORM.Core.Domain.Exceptions
{
    public class SuperOrmException : Exception
    {
        public SuperOrmException() : base()
        {

        }

        public SuperOrmException(string message) : base(message)
        {

        }
        public SuperOrmException(string message, Exception innerException) : base(message, innerException)
        {

        }

    }
}
