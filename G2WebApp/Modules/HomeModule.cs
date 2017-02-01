using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using G2WebApp.Core.Data.Entities;
using G2WebApp.Core.Data.ViewModels;
using G2WebApp.Core.Factories.Contracts;
using Nancy;

namespace G2WebApp.Modules
{
    public class HomeModule : NancyModule
    {
        private IQueryFactory queryFactory;

        public HomeModule(IQueryFactory queryFactory)
        {
            this.queryFactory = queryFactory;

            Get["/", true] = HomePage;
            Get["/search/{query}", true] = SearchQuery;
        }

        public async Task<dynamic> HomePage(dynamic param, CancellationToken token)
        {
            var stories = await queryFactory
                .CreateStoryQuery()
                .Look(s => s.Flag == FlagType.Approved)
                .BindVotes(Context.CurrentUser?.UserName)
                .FindAllAsync();

            return View["Home/Index", stories];
        }

        public async Task<dynamic> SearchQuery(dynamic param, CancellationToken token)
        {
            var query = (string)param.query;
            var stories = await queryFactory
                .CreateStoryQuery()
                .Look(s => (s.Tags.Contains(query) || s.Title.Contains(query) || s.Text.Contains(query)) &&
                           s.Flag == FlagType.Approved)
                .BindVotes(Context.CurrentUser?.UserName)
                .FindAllAsync();

            var model = new SearchResultViewModel
            {
                Query = query,
                PostViewModels = stories.ToList()
            };

            return View["Shared/SearchResult", model];
        }
    }
}
