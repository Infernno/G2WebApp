using G2WebApp.Core.BackgroundTasks;
using G2WebApp.Core.Configuration.Contracts;

namespace G2WebApp.Core.Configuration
{
    public abstract class BootstrapperBase : IBootstrapper
    {
        protected BootstrapperBase() { }

        public void Initialize()
        {
            JobManager.RunTasks();
            OnApplicationStartup();
        }

        protected virtual void OnApplicationStartup() { }
    }
}
