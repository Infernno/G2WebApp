using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using G2WebApp.Core.Data.Entities;
using G2WebApp.Core.Data.ViewModels;
using G2WebApp.Core.Factories.Contracts;
using G2WebApp.Core.Services.Contracts;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using Nancy.Validation;

namespace G2WebApp.Modules
{
    public class StoryModule : NancyModule
    {
        private IStoryService storyService;
        private IQueryFactory queryFactory;

        public StoryModule(IStoryService storyService, IQueryFactory queryFactory) : base("post")
        {
            this.storyService = storyService;
            this.queryFactory = queryFactory;

            Get["/{id}", true] = FullStory;

            Get["/add"] = x =>
            {
                this.RequiresAuthentication();
                return View["Shared/AddPost"];
            };

            Post["/add/text", true] = AddTextStory;
            Post["/add/image", true] = AddImageStory;
        }

        public async Task<dynamic> FullStory(dynamic param, CancellationToken token)
        {
            var id = (int)param.id;
            var stories = await queryFactory
                .CreateStoryQuery()
                .Look(s => s.Id == id)
                .BindVotes(Context.CurrentUser?.UserName)
                .FindFullStoryAsync();

            return View["Shared/FullPost", stories];
        }

        public async Task<dynamic> AddTextStory(dynamic param, CancellationToken token)
        {
            this.RequiresAuthentication();

            var model = this.Bind<NewTextStoryViewModel>();
            model.Author = Context.CurrentUser.UserName;

            var result = this.Validate(model);

            if (!result.IsValid)
                return new { error = "Ошибка, не все поля заполнены" };

            var post = await storyService.AddStory(model, ContentType.Text);

            if (!post.Success)
                return new { error = post.Message };

            return new { redirect = "/post/" + post.Data };
        }

        public async Task<dynamic> AddImageStory(dynamic param, CancellationToken token)
        {
            this.RequiresAuthentication();

            var file = Request.Files.FirstOrDefault();

            if (file == null)
                return new { error = "Файл не загружен" };

            var model = this.Bind<NewImageStoryViewModel>();

            model.Author = Context.CurrentUser.UserName;
            model.ImageName = file.Name;
            model.ImageStream = file.Value;

            var result = this.Validate(model);

            if (!result.IsValid)
                return new { error = "Ошибка, не все поля заполнены" };

            var post = await storyService.AddStory(model, ContentType.Image);

            if (!post.Success)
                return new { error = post.Message };

            return new { redirect = "/post/" + post.Data };
        }
    }
}
