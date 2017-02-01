using System.Threading;
using System.Threading.Tasks;
using G2WebApp.Core.Factories.Contracts;
using Nancy;

namespace G2WebApp.Modules
{
    public class ProfileModule : NancyModule
    {
        private IQueryFactory queryFactory;

        public ProfileModule(IQueryFactory queryFactory) : base("profile")
        {
            this.queryFactory = queryFactory;

            Get["/{name}", true] = DisplayProfile;
        }

        public async Task<dynamic> DisplayProfile(dynamic param, CancellationToken token)
        {
            var name = (string)param.name;

            if (string.IsNullOrEmpty(name))
                return HttpStatusCode.NotFound;

            return HttpStatusCode.NotFound;

            //    return View["Shared/ProfilePage", profile];
        }
    }
}
