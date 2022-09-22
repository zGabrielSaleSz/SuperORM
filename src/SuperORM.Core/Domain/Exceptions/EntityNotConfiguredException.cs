using System;
using System.Collections.Generic;
using System.Text;

namespace SuperORM.Core.Domain.Exceptions
{
    public class EntityNotConfiguredException : SuperOrmException
    {
        public EntityNotConfiguredException(string message) : base(message)
        {

        }
    }
}
