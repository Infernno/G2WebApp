using System.Threading.Tasks;
using G2WebApp.Core.Data.Entities;
using G2WebApp.Core.Util;

namespace G2WebApp.Core.Services.Contracts
{
    public interface IStoryService
    {
        Task<OperationResult> AddStory(object viewModel, ContentType StoryType);
        Task<OperationResult> Vote(VoteParams vote);
    }
}
