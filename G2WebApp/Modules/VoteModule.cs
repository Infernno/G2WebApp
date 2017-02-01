using System.Threading;
using System.Threading.Tasks;
using G2WebApp.Core.Services.Contracts;
using G2WebApp.Core.Util;
using Nancy;

namespace G2WebApp.Modules
{
    public class VoteModule : NancyModule
    {
        private IStoryService storyService;
        private ICommentService commentService;

        public VoteModule(IStoryService storyService, ICommentService commentService) : base("vote")
        {
            this.storyService = storyService;
            this.commentService = commentService;

            Get["/post", true] = VoteForPost;
            Get["/comment", true] = VoteForComment;
        }

        public async Task<dynamic> VoteForPost(dynamic param, CancellationToken token)
        {
            if (Context.CurrentUser == null)
                return new { error = "Авторизуйтесь для того чтобы проголосовать" };

            int storyId = int.Parse(Request.Query.Id);
            int voteValue = int.Parse(Request.Query.Value);

            var vote = new VoteParams(storyId, voteValue, Context.CurrentUser.UserName);
            var result = await storyService.Vote(vote);

            if (result.Success)
                return new { overallRating = result.Data };

            return new { error = result.Message };
        }

        public async Task<dynamic> VoteForComment(dynamic param, CancellationToken token)
        {
            if (Context.CurrentUser == null)
                return new { error = "Авторизуйтесь для того чтобы проголосовать" };

            int commentId = int.Parse(Request.Query.Id);
            int voteValue = int.Parse(Request.Query.Value);

            var vote = new VoteParams(commentId, voteValue, Context.CurrentUser.UserName);
            var result = await commentService.Vote(vote);

            if (result.Success)
                return new { overallRating = result.Data };

            return new { error = result.Message };
        }
    }
}
