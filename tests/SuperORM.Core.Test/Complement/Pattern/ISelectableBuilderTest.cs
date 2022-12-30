using Xunit;

namespace SuperORM.Core.Test.Complement.Pattern
{
    public interface ISelectableBuilderTest
    {
        [Fact]
        void Should_BuildQuery_When_SelectableUsingHaving();
        [Fact]
        void Should_BuildQuery_When_SelectableUsingGroupBy();

        [Fact]
        void Should_BuildQuery_When_SelectableUsingLimit();

        [Fact]
        void Should_BuildQuery_When_SelectableUsingOrderBy();

        [Fact]
        void Should_BuildQuery_When_SelectableUsingWhere();

        [Fact]
        void Should_BuildQuery_When_SelectableUsingJoin();

        [Fact]
        void Should_BuildQuery_When_SelectableUsingFieldsBuilder();

        [Fact]
        void Should_BuildQuery_When_SelectableUsingString();

        [Fact]
        void Should_SpecifyTable_When_UsingQueryBuilder();

        [Fact]
        void Should_BuildQuerySpecifingTabeInSelect_When_UsingAliasInTableQueryBuilder();
    }
}
