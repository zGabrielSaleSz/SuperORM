
using SuperORM.Core.Domain.Model.QueryBuilder;
using SuperORM.Core.Domain.Model.QueryBuilder.Fields;

namespace SuperORM.Core.Interface.QueryBuilder
{
    public interface IUpdatableBuilder : IQueryBuilder, IWhereable<IUpdatableBuilder>
    {
        IUpdatableBuilder Update(Table table);
        IUpdatableBuilder Set(IField field, IField field2);
        IUpdatableBuilder Set<T>(IField field, T value);
    }
}
