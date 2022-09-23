using System;
using System.Collections.Generic;
using System.Text;

namespace SuperORM.Core.Domain.Exceptions
{
    public class DuplicatedRepositoryImplementation : SuperOrmException
    {
        public DuplicatedRepositoryImplementation(string message) : base(message)
        {

        }
    }
}
