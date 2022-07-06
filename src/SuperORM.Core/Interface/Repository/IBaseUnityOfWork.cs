using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperORM.Core.Interface.Repository
{
    public interface IBaseUnityOfWork
    {
        void UseTransaction();
        void Commit();
        void Rollback();
    }
}
