using G2WebApp.Core.DependencyManagement.Contracts;
using Nancy.TinyIoc;

namespace G2WebApp.App.Config
{
    public class TinyIocResolver : IDependencyResolver
    {
        private TinyIoCContainer container;

        public TinyIocResolver(TinyIoCContainer container)
        {
            this.container = container;
        }

        public T Resolve<T>() where T : class
        {
            return container.Resolve<T>();
        }
    }
}
