using G2WebApp.Auth.Core;
using Nancy;

namespace G2WebApp.Auth.Extensions
{
    public static class ModuleExtensions
    {
        public static Response LoginAndRedirect(this INancyModule module, string username)
        {
            return FormsAuthentication.LoginResponse(module.Context, username);
        }

        public static Response LogoutAndRedirect(this INancyModule module)
        {
            return FormsAuthentication.LogoutResponse(module.Context);
        }
    }
}
