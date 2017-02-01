using System.Threading;
using System.Threading.Tasks;
using G2WebApp.Core.Data.ViewModels;
using G2WebApp.Core.Services.Contracts;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Validation;

namespace G2WebApp.Modules
{
    public class CommentModule : NancyModule
    {
        private ICommentService commentService;

        public CommentModule(ICommentService commentService) : base("comment")
        {
            this.commentService = commentService;

            Post["/add", true] = AddComment;
        }

        public async Task<dynamic> AddComment(dynamic param, CancellationToken token)
        {
            if (Context.CurrentUser == null)
                return new { error = "Вы должны быть авторизаны для комментирования" };

            var id = (int)Request.Query.Id;

            var model = this.Bind<AddCommentViewModel>();

            model.Author = Context.CurrentUser.UserName;
            model.StoryID = id;

            var result = this.Validate(model);

            if (!result.IsValid)
                return new { error = "Комментарий пустой!" };

            var comment = await commentService.AddComment(model);

            return !comment.Success ? (dynamic) new { error = comment.Message } : new { message = comment.Data };
        }
    }
}
