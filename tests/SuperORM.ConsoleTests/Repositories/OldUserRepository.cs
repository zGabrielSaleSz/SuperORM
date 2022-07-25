using SuperORM.Core.Domain.Model.Repository;
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
            SetTable("oldUsers");
            SetPrimaryKey(u => u.ID);
        }

        public override void ConfigurateColumns(IPropertyConfiguration<OldUser> propertyConfiguration)
        {
            propertyConfiguration
                .SetColumnName(u => u.ID, "id")
                .SetColumnName(u => u.Name, "strName")
                .SetColumnName(u => u.Email, "strEmail")
                .SetColumnName(u => u.Password, "strPassword")
                .SetColumnName(u => u.Active, "blnActive")
                .SetColumnName(u => u.ApprovedDate, "approvedDt");
        }
    }
}
