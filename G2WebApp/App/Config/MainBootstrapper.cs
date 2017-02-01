using System;
using G2WebApp.Auth.Core;
using G2WebApp.Core.Configuration;
using G2WebApp.Core.Data.Contracts;
using G2WebApp.Core.Data.Repositories;
using G2WebApp.Core.DependencyManagement;
using G2WebApp.Core.Queries;
using G2WebApp.Core.Queries.Contracts;
using G2WebApp.Core.Services;
using G2WebApp.Core.Services.Contracts;
using G2WebApp.Core.Util;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Cryptography;
using Nancy.TinyIoc;
using Nancy.ViewEngines;
using Nancy.ViewEngines.Razor;

namespace G2WebApp.App.Config
{
    public class MainBootstrapper : DefaultNancyBootstrapper
    {
        protected override byte[] FavIcon => null;

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            Conventions.ViewLocationConventions.Add(((viewName, model, context) => string.Concat("Views/", viewName)));

            StaticConfiguration.Caching.EnableRuntimeViewUpdates = true;
            StaticConfiguration.DisableErrorTraces = false;

            var resolver = new TinyIocResolver(container);
            DependencyResolver.Current = resolver;

            var bootstrapper = new DefaultBootstrapper();
            bootstrapper.Initialize();

            var config = new FormsAuthenticationConfiguration
            {
                RedirectUrl = "/",
                CookieExpiryDays = TimeSpan.FromDays(30),
                CryptographyConfiguration = CryptographyConfiguration.Default,
                UserRepository = container.Resolve<IUserRepository>()
            };

            pipelines.BeforeRequest.AddItemToStartOfPipeline(ctx =>
            {
                var ip = ctx.Request.UserHostAddress;
                var isBanned = VerificationService.Current.isUserBanned(ip);

                return isBanned ? Response.NoBody : ctx.Response;
            });

            pipelines.AfterRequest += ctx =>
            {
                Debug.Log(ctx.Request.Method + " request from '" + ctx.Request.UserHostAddress + "' to " + ctx.Request.Url);
            };

            pipelines.OnError += (ctx, ex) =>
            {
                Debug.LogError("Unhandled error on request '" + ctx.Request.Url + "' : " + ex);
                return null;
            };

            FormsAuthentication.Enable(pipelines, config);
            base.ApplicationStartup(container, pipelines);
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Scripts", @"Scripts", "js", "map"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("fonts", @"fonts", "eot", "svg", "ttf", "woff", "woff2"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Images", @"Images", "png", "gif", "jpg"));

            base.ConfigureConventions(nancyConventions);
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            container.Register<IUserRepository, UserRepository>().AsMultiInstance();
            container.Register<IProfileRepository, ProfileRepository>().AsMultiInstance();
            container.Register<IStoryRepository, StoryRepository>().AsMultiInstance();
            container.Register<ICommentRepository, CommentRepository>().AsMultiInstance();
            container.Register<IVoteRepository, VoteRepository>().AsMultiInstance();
            container.Register<IStoryQuery, StoryQuery>().AsMultiInstance();

            container.Register<IAuthService, AuthService>().AsSingleton();
            container.Register<IStoryService, StoryService>().AsSingleton();
            container.Register<ICommentService, CommentService>().AsSingleton();

            container.Register<IViewEngine, RazorViewEngine>();

            base.ConfigureApplicationContainer(container);
        }
    }
}
