using G2WebApp.Core.Queries.Contracts;

namespace G2WebApp.Core.Factories.Contracts
{
    public interface IQueryFactory
    {
        IStoryQuery CreateStoryQuery();
    }
}
