using SuperORM.Core.Domain.Model.QueryBuilder;
using System;

namespace SuperORM.Core.Domain.Model.LinqSQL
{
    public interface IJoinAdapter<T> 
    {
        string GetAlias();
    }
    public class JoinAdapter<T> : IJoinAdapter<T>
    {
        private string _tag;
        public JoinAdapter(string tag) {
            _tag = tag + $"{tag}_{Guid.NewGuid()}";
        }

        public string GetAlias()
        {
            return _tag;
        }


    }
}
