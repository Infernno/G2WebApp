using G2WebApp.Core.Data.Contracts;
using G2WebApp.Core.DependencyManagement;
using Nancy.ViewEngines.Razor;

namespace G2WebApp.App.Helpers
{
    public static class HtmlHelper
    {
        private static IProfileRepository profileRepository = DependencyResolver.Current.Resolve<IProfileRepository>();

        public static object ShowProfile<T>(this HtmlHelpers<T> helpers, string Username)
        {
            if (string.IsNullOrEmpty(Username))
                return null;

            return profileRepository.Find(p => p.DisplayName == Username);
        }

        public static object CommentOfDay<T>(this HtmlHelpers<T> helpers)
        {
            return null;
        }
    }
}
