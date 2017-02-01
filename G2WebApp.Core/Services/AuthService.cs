using System.Threading.Tasks;
using AutoMapper;
using G2WebApp.Core.Data.Contracts;
using G2WebApp.Core.Data.Entities;
using G2WebApp.Core.Data.ViewModels;
using G2WebApp.Core.Services.Contracts;
using G2WebApp.Core.Util;
using Profile = G2WebApp.Core.Data.Entities.Profile;

namespace G2WebApp.Core.Services
{
    public class AuthService : IAuthService
    {
        private IUserRepository userRepository;
        private IProfileRepository profileRepository;

        private static readonly object syncLock = new object();

        public AuthService(IUserRepository userRepository, IProfileRepository profileRepository)
        {
            this.userRepository = userRepository;
            this.profileRepository = profileRepository;
        }

        private OperationResult LoginInternal(LoginViewModel viewModel)
        {
            lock (syncLock)
            {
                var authResult = userRepository.Exists(u => u.Username == viewModel.Username && u.Password == viewModel.Password);

                return authResult ? OperationResult.Successful() : OperationResult.Failed();
            }
        }

        private OperationResult RegisterInternal(RegisterViewModel viewModel)
        {
            lock (syncLock)
            {
                var userEntity = Mapper.Map<User>(viewModel);
                var profileEntity = Mapper.Map<Profile>(viewModel);

                var isUserCreated = userRepository.Add(userEntity);
                var isProfileCreated = profileRepository.Add(profileEntity);

                return isUserCreated && isProfileCreated ? OperationResult.Successful() : OperationResult.Failed();
            }
        }

        public Task<OperationResult> SignInAsync(LoginViewModel viewModel)
        {
            return Task.Factory.StartNew(() => LoginInternal(viewModel));
        }

        public Task<OperationResult> RegisterAsync(RegisterViewModel viewModel)
        {
            return Task.Factory.StartNew(() => RegisterInternal(viewModel));
        }
    }
}
