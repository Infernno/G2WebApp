using System;
using System.Collections.Generic;
using G2WebApp.Core.DependencyManagement.Contracts;

namespace G2WebApp.Core.DependencyManagement
{
    public class SimpleIoc : IDependencyResolver
    {
        private static SimpleIoc instance;
        private static object syncLock = new object();

        private Dictionary<Type, object> container = new Dictionary<Type, object>();

        public static SimpleIoc Current
        {
            get
            {
                if (instance == null)
                {
                    lock (syncLock)
                    {
                        instance = new SimpleIoc();
                    }
                }

                return instance;
            }
        }

        public void Register<T>(T toRegister) where T : class
        {
            lock (syncLock)
            {
                container.Add(typeof (T), toRegister);
            }
        }

        public T Resolve<T>() where T : class
        {
            return (T)container[typeof(T)];
        }
    }
}
