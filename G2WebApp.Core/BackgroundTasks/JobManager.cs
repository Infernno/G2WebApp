using System;
using System.Collections.Generic;
using System.Threading;
using G2WebApp.Core.Extensions;
using G2WebApp.Core.Util;

namespace G2WebApp.Core.BackgroundTasks
{
    public static class JobManager
    {
        // ReSharper disable once CollectionNeverQueried.Local
        private static List<Job> tasks = new List<Job>();
        private static bool areLaunched;

        public static void RunTasks()
        {
            if (areLaunched)
                return;

            Debug.Log("Launching tasks..");

            try
            {
                var jobs = AssemblyScanner.TypesOf(typeof(Job));

                foreach (var job in jobs)
                {
                    if (!isMatching(job))
                        continue;

                    var task = (Job)job.GetInstance();
                    var thread = new Thread(task.Execute) { IsBackground = true };

                    tasks.Add(task);
                    thread.Start();

                    Debug.Log("Task \"{0}\" has been instantiated successfully on thread #{1}", task.Name, thread.ManagedThreadId);
                }

                foreach (var task in tasks)
                {

                }
            }
            catch (Exception ex)
            {
                Debug.LogError("An error has occured while instantiating tasks.");
                Debug.LogError(ex);
            }

            areLaunched = true;
        }

        private static bool isMatching(Type type)
        {
            return type.IsAbstract == false
                && type.IsGenericTypeDefinition == false
                && type.IsInterface == false;
        }
    }
}
