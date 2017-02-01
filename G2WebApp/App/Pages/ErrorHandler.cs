using Nancy;
using Nancy.ErrorHandling;
using Nancy.ViewEngines;

namespace G2WebApp.App.Pages
{
    public class ErrorHandler : DefaultViewRenderer, IStatusCodeHandler
    {
        public ErrorHandler(IViewFactory factory) : base(factory)
        {
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return statusCode == HttpStatusCode.NotFound
                   || statusCode == HttpStatusCode.Unauthorized
                   || statusCode == HttpStatusCode.InternalServerError;
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            var response = RenderView(context, "Views/Errors/" + (int)statusCode + ".cshtml");
            response.StatusCode = HttpStatusCode.NotFound;
            context.Response = response;
        }
    }
}
