using SuperORM.Core.Domain.Service.LinqSQL;
using SuperORM.Core.Domain.Service.LinqSQL.SelectableTools;
using SuperORM.Core.Interface;
using SuperORM.Core.Interface.LinqSQL;
using SuperORM.TestsResource.Entities;
using SuperORM.TestsResource.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperORM.ConsoleTests.UseCases
{
    internal class ColumnAssimilationTests
    {
        internal static void RunRepository()
        {
            OldUserRepository oldUserRepository = new OldUserRepository();

            List<OldUser> result = oldUserRepository
                .GetSelectable()
                .OrderBy(u => u.ID)
                .AsEnumerable()
                .ToList();

            OldUser oldUser = new OldUser();
            oldUser.Email = "superorm@hotmail.com";
            oldUser.Name = "ColumnAssimilator";
            oldUser.Password = "SüP3Rs3Cr3t";
            oldUser.Active = false;
            oldUser.ApprovedDate = DateTime.Now;
            oldUserRepository.Insert(oldUser);

            oldUser.Active = true;
            oldUser.ApprovedDate = DateTime.Now.AddDays(-1);
            oldUser.Email = "gabriel.s479@hotmail.com";
            oldUserRepository.Update(oldUser);

            oldUserRepository.Delete(oldUser);
        }

        internal static void RunLinqToSql(IConnectionProvider connectionProvider)
        {
            IConnection connection = connectionProvider.GetNewConnection();
            IQuerySintax querySintax = connectionProvider.GetQuerySintax();

            ColumnAssimilator columnAssimilator = new ColumnAssimilator();
            columnAssimilator.Add<OldUser>("ID", "id");
            columnAssimilator.Add<OldUser>("Name", "strName");
            columnAssimilator.Add<OldUser>("Email", "strEmail");
            columnAssimilator.Add<OldUser>("Password", "strPassword");
            columnAssimilator.Add<OldUser>("Active", "blnActive");
            columnAssimilator.Add<OldUser>("ApprovedDate", "approvedDt");

            var selectable = new Selectable<OldUser>(connection, querySintax);
            var query = selectable
                .AddColumnAssimilation(columnAssimilator)
                .SelectAll()
                .From("oldUsers")
                .Where(u => u.ID == 1 || u.Name.Contains("gabriel") || u.Active == true)
                .OrderBy(u => u.ID).OrderByDescending(u => u.Name);
            string queryResult = query.GetQuery();
            OldUser result = query.FirstOrDefault();


            var insertable = new Insertable<OldUser>(connection, querySintax);
            OldUser values = new OldUser
            {
                Name = "Super Orm",
                Email = "gabriel.s479@hotmail.com",
                Active = true,
                Password = "SUPERAw3s0meP4ssw0rd",
                ApprovedDate = DateTime.Now,
            };
            IInsertable<OldUser> insertQuery = insertable
                .AddColumnAssimilation(columnAssimilator)
                .Into("oldUsers")
                .Ignore("ID")
                .Values(values);

            string insertQueryResult = insertQuery.GetQuery();
            int insertedRowsChanged = insertQuery.Execute();

            var updatable = new Updatable<OldUser>(connection, querySintax);
            IUpdatable<OldUser> updatableQuery = updatable
                .AddColumnAssimilation(columnAssimilator)
                .Update("oldUsers")
                .Set(a => a.Active, false)
                .Where(u => u.Email == "gabriel.s479@hotmail.com");
            string updatableQueryResult = updatable.GetQuery();
            int updatableRowsChanged = updatable.Execute();

            var deletable = new Deletable<OldUser>(connection, querySintax);
            IDeletable<OldUser> deletableQuery = deletable
                .AddColumnAssimilation(columnAssimilator)
                .From("oldUsers")
                .Where(a => a.ID < 11);

            string deletableQueryResult = deletable.GetQuery();
            int deletableRowsChanged = deletable.Execute();
        }
    }
}
