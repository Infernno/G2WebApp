using G2WebApp.Core.DependencyManagement;
using G2WebApp.Core.Factories.Contracts;
using G2WebApp.Core.Queries.Contracts;

namespace G2WebApp.Core.Factories
{
    public class QueryFactory : IQueryFactory
    {
        public IStoryQuery CreateStoryQuery()
        {
            return DependencyResolver.Current.Resolve<IStoryQuery>();
        }
    }
}
