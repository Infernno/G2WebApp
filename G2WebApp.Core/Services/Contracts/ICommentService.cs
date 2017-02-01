using System.Threading.Tasks;
using G2WebApp.Core.Data.ViewModels;
using G2WebApp.Core.Util;

namespace G2WebApp.Core.Services.Contracts
{
    public interface ICommentService
    {
        Task<OperationResult> AddComment(AddCommentViewModel model);
        Task<OperationResult> Vote(VoteParams vote);
    }
}
