namespace G2WebApp.Core.DependencyManagement.Contracts
{
    public interface IDependencyResolver
    {
        T Resolve<T>() where T : class;
    }
}
