using System.Collections.Generic;

namespace SuperORM.Core.Interface.QueryBuilder
{
    public interface IResultAvailable<T>
    {
        T FirstOrDefault();
        IEnumerable<T> ToIEnumerable();
    }
}
