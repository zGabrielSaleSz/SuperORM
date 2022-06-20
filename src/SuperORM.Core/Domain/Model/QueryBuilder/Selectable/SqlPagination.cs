using SuperORM.Core.Domain.Exceptions;
using SuperORM.Core.Interface;

namespace SuperORM.Core.Domain.Model.QueryBuilder.Selectable
{
    public class SqlPagination
    {
        private readonly IQuerySintax querySintax;
        private uint SkipRowsAmount = 0;
        private uint? TakeRowsAmount = null;
        public SqlPagination(IQuerySintax querySintax)
        {
            this.querySintax = querySintax;
        }

        public void Skip(uint rows)
        {
            SkipRowsAmount = rows;
        }
        public void Take(uint rows)
        {
            this.TakeRowsAmount = rows;
        }

        public string GetPagination()
        {
            if (IsIncomplete())
                throw new SqlDriverExpressionNotSupportedException("You should use Take() method if Skip() was informed");

            if ((querySintax.IsTopAvailable() && CanUseTop()) || IsEmpty())
            {
                return string.Empty;
            }

            if (CanUseTop())
            {
                return querySintax.GetPaginationSintax(TakeRowsAmount.Value);
            }
            return querySintax.GetPaginationSintax(SkipRowsAmount, TakeRowsAmount.Value);
        }

        public bool HasSkipRows()
        {
            return (SkipRowsAmount > 0);
        }

        public bool HasTakeRows()
        {
            return TakeRowsAmount.HasValue && TakeRowsAmount != 0;
        }

        public bool IsEmpty()
        {
            return (!HasTakeRows() && !HasSkipRows());
        }

        private bool IsIncomplete()
        {
            return (HasSkipRows() && !HasTakeRows());
        }

        private bool CanUseTop()
        {
            return (!HasSkipRows() && HasTakeRows());
        }

        public string GetTop()
        {
            if (querySintax.IsTopAvailable() && CanUseTop())
                return $"TOP({TakeRowsAmount}) ";
            return string.Empty;
        }
    }
}
