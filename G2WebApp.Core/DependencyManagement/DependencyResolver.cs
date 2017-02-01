using System;
using G2WebApp.Core.DependencyManagement.Contracts;

namespace G2WebApp.Core.DependencyManagement
{
    public static class DependencyResolver
    {
        private static IDependencyResolver currentResolver;
        private static object syncLock = new object();

        public static IDependencyResolver Current
        {
            get
            {
                if(currentResolver == null)
                    throw new NullReferenceException("Dependency resolver is not initialized!");

                return currentResolver;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                lock (syncLock)
                {
                    currentResolver = value;
                }
            }
        }
    }
}
