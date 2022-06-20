using System;

namespace SuperORM.Core.Domain.Exceptions
{
    public class TableReferenceException : Exception
    {
        public TableReferenceException(string message) : base(message)
        {

        }
    }
}
