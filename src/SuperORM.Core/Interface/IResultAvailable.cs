using System.Collections.Generic;

namespace SuperORM.Core.Interface
{
    public interface IResultAvailable<T>
    {
        T FirstOrDefault();
        IEnumerable<T> ToIEnumerable();
    }
}
