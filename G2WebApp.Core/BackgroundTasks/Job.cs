using System;
using System.Threading;

namespace G2WebApp.Core.BackgroundTasks
{
    public abstract class Job
    {
        // ReSharper disable once ConvertPropertyToExpressionBody
        public virtual string Name { get { return GetType().Name; } }
        public abstract bool IsRepeatable { get; }
        public abstract TimeSpan RepetitionIntervalTime { get; }

        private bool shutDown;

        public abstract void Action();
        public virtual void OnStartup() { }

        public void Execute()
        {
            OnStartup();

            if (IsRepeatable)
            {
                while (!shutDown)
                {
                    Action();
                    Thread.Sleep(RepetitionIntervalTime);
                }
            }
            else
                Action();
        }

        public void Stop()
        {
            shutDown = true;
        }
    }
}
