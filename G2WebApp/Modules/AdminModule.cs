using System.Linq;
using G2WebApp.Core.Data.Contracts;
using G2WebApp.Core.Data.Entities;
using G2WebApp.Core.Extensions;
using Nancy;
using Nancy.Security;

namespace G2WebApp.Modules
{
    public class AdminModule : NancyModule
    {
        private IStoryRepository storyRepository;
        private ICommentRepository commentRepository;
        private IUserRepository userRepository;

        public AdminModule(IStoryRepository storyRepository, ICommentRepository commentRepository, IUserRepository userRepository)
            : base("admin")
        {
            this.RequiresAuthentication();
            this.RequiresClaims(UserGroup.Administrator.ToDescription());

            this.storyRepository = storyRepository;
            this.commentRepository = commentRepository;
            this.userRepository = userRepository;

            Get["/"] = x => View["Admin/Main"];
            Get["/showAllPosts"] = ShowAllPosts;
            Get["/showAllComments"] = ShowAllComments;
            Get["/showAllUsers"] = ShowAllUsers;
            Get["/do"] = ApproveOrDeletePost;
        }

        public dynamic ShowAllPosts(dynamic param)
        {
            var posts = storyRepository.FindAll().OrderBy(x => x.Flag).ToList();
            return View["Admin/ShowAllStories", posts];
        }

        public dynamic ShowAllComments(dynamic param)
        {
            var comments = commentRepository.FindAll().OrderBy(x => x.Flag).ToList();
            return View["Admin/ShowAllComments", comments];
        }

        public dynamic ShowAllUsers(dynamic param)
        {
            var users = userRepository.FindAll(u => u.Role != UserGroup.Administrator).OrderBy(x => x.Role).ToList();
            return View["Admin/ShowAllUsers", users];
        }

        public dynamic ApproveOrDeletePost(dynamic param)
        {
            var type = (string)Request.Query.Type;
            var action = Request.Query.Action.ToString();
            var id = (int)Request.Query.Id;

            if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(action))
                return Response.AsRedirect("/admin");

            if (type.Equals("post"))
            {
                if (action.Equals("approve"))
                    storyRepository.UpdateManually(p => p.Id == id, p => p.Flag, FlagType.Approved);
                else if (action.Equals("delete"))
                    storyRepository.UpdateManually(p => p.Id == id, p => p.Flag, FlagType.Delete);
                return Response.AsRedirect("/admin/showAllPosts");
            }
            if (type.Equals("comment"))
            {
                if (action.Equals("approve"))
                    commentRepository.UpdateManually(p => p.Id == id, p => p.Flag, FlagType.Approved);
                else if (action.Equals("delete"))
                    commentRepository.UpdateManually(p => p.Id == id, p => p.Flag, FlagType.Delete);
                return Response.AsRedirect("/admin/showAllComments");
            }
            if (type.Equals("user"))
            {
                if (action.Equals("ban"))
                    userRepository.UpdateManually(p => p.Id == id, p => p.Role, UserGroup.Banned);
                else if (action.Equals("unban"))
                    userRepository.UpdateManually(p => p.Id == id, p => p.Role, UserGroup.User);
                else if (action.Equals("setCategory"))
                {
                    int catId = int.Parse(Request.Query.catId);
                    userRepository.UpdateManually(p => p.Id == id, p => p.Role, (UserGroup)catId);
                }

                return Response.AsRedirect("/admin/showAllUsers");
            }

            return Response.AsRedirect("/admin");
        }
    }
}
