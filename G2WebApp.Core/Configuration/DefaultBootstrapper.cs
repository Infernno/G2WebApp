using AutoMapper;
using G2WebApp.Core.Data.Entities;
using G2WebApp.Core.Data.ViewModels;
using Profile = G2WebApp.Core.Data.Entities.Profile;

namespace G2WebApp.Core.Configuration
{
    public class DefaultBootstrapper : BootstrapperBase
    {
        protected override void OnApplicationStartup()
        {
            Mapper.CreateMap<LoginViewModel, User>();

            Mapper.CreateMap<RegisterViewModel, User>();
            Mapper.CreateMap<RegisterViewModel, Profile>()
                .ForMember(p => p.DisplayName, opts => opts.MapFrom(m => m.Username));

            Mapper.CreateMap<Comment, CommentViewModel>();
            Mapper.CreateMap<AddCommentViewModel, Comment>();

            Mapper.CreateMap<StoryViewModel, Story>();
            Mapper.CreateMap<Story, StoryViewModel>();

            Mapper.CreateMap<NewTextStoryViewModel, Story>();
            Mapper.CreateMap<NewImageStoryViewModel, Story>();
        }
    }
}
