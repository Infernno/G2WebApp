using System;
using G2WebApp.Auth.Identity;
using G2WebApp.Core.Extensions;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Extensions;
using Nancy.Helpers;

namespace G2WebApp.Auth.Core
{
    public static class FormsAuthentication
    {
        #region Main

        private const string AuthCookieName = "X25jZmE";

        // ReSharper disable once InconsistentNaming
        private static FormsAuthenticationConfiguration config;

        public static void Enable(IPipelines pipelines, FormsAuthenticationConfiguration configuration)
        {
            if (pipelines == null)
                throw new ArgumentException(nameof(pipelines));
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            config = configuration;

            pipelines.BeforeRequest.AddItemToStartOfPipeline(GetLoadAuthenticationHook());
            pipelines.AfterRequest.AddItemToEndOfPipeline(GetRedirectToLoginHook());
        }

        private static Func<NancyContext, Response> GetLoadAuthenticationHook()
        {
            return context =>
            {
                var username = GetAuthenticatedUserFromCookie(context);

                if (!string.IsNullOrEmpty(username))
                {
                    var user = config.UserRepository.Find(u => u.Username == username);

                    if (user != null)
                    {
                        context.CurrentUser = new UserIdentity()
                        {
                            UserName = user.Username,
                            Claims = new[] { user.Role.ToDescription() }
                        };
                    }
                }

                return null;
            };
        }

        private static Action<NancyContext> GetRedirectToLoginHook()
        {
            return context =>
            {
                if (context.Response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    context.Response = HttpStatusCode.Unauthorized;
                }
            };
        }

        #endregion

        #region Responses 

        public static Response LoginResponse(NancyContext context, string username)
        {
            var expiryDate = DateTime.Now.Add(config.CookieExpiryDays);
            var response = context.GetRedirect(config.RedirectUrl);
            var cookie = CookieBuilder.BuildCookie(AuthCookieName, username, expiryDate, config.CryptographyConfiguration.EncryptionProvider);

            response.WithCookie(cookie);

            return response;
        }

        public static Response LogoutResponse(NancyContext context)
        {
            var response = context.GetRedirect(config.RedirectUrl);
            var cookie = CookieBuilder.BuildLogOutCookie(AuthCookieName);

            response.WithCookie(cookie);

            return response;
        }

        #endregion

        #region Cookie handler 

        private static string GetAuthenticatedUserFromCookie(NancyContext context)
        {
            if (!context.Request.Cookies.ContainsKey(AuthCookieName)) return string.Empty;

            var encryptedCookieValue = context.Request.Cookies[AuthCookieName];

            if (string.IsNullOrEmpty(encryptedCookieValue)) return string.Empty;

            var cookieValue = DecryptAuthCookie(encryptedCookieValue);

            return string.IsNullOrEmpty(cookieValue) ? string.Empty : cookieValue;
        }

        private static string DecryptAuthCookie(string value)
        {
            var decodedCookie = HttpUtility.UrlDecode(value);
            var cookieValue = config.CryptographyConfiguration.EncryptionProvider.Decrypt(decodedCookie);

            return cookieValue ?? string.Empty;
        }

        #endregion
    }
}