using SuperORM.Core.Domain.Service.Repository;
using SuperORM.Core.Test.Complement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperORM.ConsoleTests.Repositories
{
    public class OldUserRepository : BaseRepository<OldUser, int>
    {
        public override void Configurate()
        {
            SetTable("oldUser");
            SetPrimaryKey(u => u.ID);
        }
    }
}
