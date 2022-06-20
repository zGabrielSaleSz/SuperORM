using SuperORM.Core.Utilities;
using System.Collections.Generic;

namespace SuperORM.Core.Domain.Service.LinqSQL.SelectableTools
{
    public class ColumnAssimilator
    {
        private static ColumnAssimilator EmptyInstance = new ColumnAssimilator();
        private readonly TwoWayKeyDicionary<string> columnsEquivalent;

        private ColumnAssimilator()
        {
            this.columnsEquivalent = new TwoWayKeyDicionary<string>();
        }
        public ColumnAssimilator(Dictionary<string, string> columnsEquivalent)
        {
            this.columnsEquivalent = new TwoWayKeyDicionary<string>();
            this.columnsEquivalent.AddRange(columnsEquivalent);
        }

        public static ColumnAssimilator Empty
        {
            get
            {
                return EmptyInstance;
            }
        }

        public string GetRespective(string column)
        {
            return columnsEquivalent.GetRespective(column);
        }
    }
}
