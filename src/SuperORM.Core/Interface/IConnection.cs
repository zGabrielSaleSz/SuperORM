using SuperORM.Core.Domain.Model.Sql;
using System;
using System.Collections.Generic;

namespace SuperORM.Core.Interface
{
    public interface IConnection
    {
        int ExecuteNonQuery(CommandContext commandContext);
        ExecuteScalarResult<T> ExecuteScalar<T>(CommandContext commandContext);
        IEnumerable<IDictionary<string, object>> ExecuteReader(CommandReaderContext readerContext);
    }
}
