using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperORM.Core.Interface
{
    public interface ITransactionConnection : IConnection
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
