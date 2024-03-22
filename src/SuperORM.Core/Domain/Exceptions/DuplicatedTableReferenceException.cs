using System;
using System.Collections.Generic;
using System.Text;

namespace SuperORM.Core.Domain.Exceptions
{
    public class DuplicatedTableReferenceException : SuperOrmException
    {
        public DuplicatedTableReferenceException(string message) : base(message) { }
    }
}
