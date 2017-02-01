using System.Threading;
using System.Threading.Tasks;
using G2WebApp.Auth.Extensions;
using G2WebApp.Core.Data.ViewModels;
using G2WebApp.Core.Services.Contracts;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Validation;

namespace G2WebApp.Modules
{
    public class AuthModule : NancyModule
    {
        private IAuthService authService;

        public AuthModule(IAuthService authService) : base("auth")
        {
            this.authService = authService;

            Post["/signin", true] = SignInAsync;
            Post["/signup", true] = RegisterAsync;
            Get["/logout"] = x => this.LogoutAndRedirect();
        }

        public async Task<dynamic> SignInAsync(dynamic param, CancellationToken cancellationToken)
        {
            var model = this.Bind<LoginViewModel>();
            var result = this.Validate(model);

            if (!result.IsValid)
                return new { error = "Ошибка, не все поля заполнены." };

            var loginOperation = await authService.SignInAsync(model);

            if (!loginOperation.Success)
                return new { error = "Неправильный логин или пароль" };

            return this.LoginAndRedirect(model.Username);
        }


        public async Task<dynamic> RegisterAsync(dynamic param, CancellationToken cancellationToken)
        {
            var model = this.Bind<RegisterViewModel>();
            model.Ip = Request.UserHostAddress;

            var result = this.Validate(model);

            if (!result.IsValid)
                return new { error = "Ошибка, не все поля заполнены." };

            var registrationResult = await authService.RegisterAsync(model);

            if (!registrationResult.Success)
                return new { error = "Ошибка регистрации, возможно такой пользователь уже существует." };

            return this.LoginAndRedirect(model.Username);
        }
    }
}