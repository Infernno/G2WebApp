using System.Threading.Tasks;
using G2WebApp.Core.Data.ViewModels;
using G2WebApp.Core.Util;

namespace G2WebApp.Core.Services.Contracts
{
    public interface IAuthService
    {
        Task<OperationResult> SignInAsync(LoginViewModel viewModel);
        Task<OperationResult> RegisterAsync(RegisterViewModel viewModel);
    }
}
