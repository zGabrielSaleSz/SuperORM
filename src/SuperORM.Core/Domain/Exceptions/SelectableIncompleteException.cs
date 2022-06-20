using System;

namespace SuperORM.Core.Domain.Exceptions
{
    public class SelectableIncompleteException : Exception
    {
        public SelectableIncompleteException(string message) : base(message)
        {
        }
    }
}
