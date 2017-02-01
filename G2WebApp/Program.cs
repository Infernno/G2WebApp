using System;
using System.Threading;
using G2WebApp.App.Config;
using G2WebApp.Core.Util;
using Nancy.Hosting.Self;

namespace G2WebApp
{
    internal static class Program
    {
        internal static AutoResetEvent QuitEvent { get; } = new AutoResetEvent(false);

        private static void Main()
        {
            var url = new Uri("http://127.0.0.1:8080");
            var config = new HostConfiguration { UrlReservations = new UrlReservations { CreateAutomatically = true } };
            var bootstrapper = new MainBootstrapper();

            Debug.Log("Starting website on " + url);
            Debug.Log("Main thread ID is #" + Thread.CurrentThread.ManagedThreadId);

            using (var host = new NancyHost(url, bootstrapper, config))
            {
                host.Start();
                QuitEvent.WaitOne();
                host.Stop();
            }
        }
    }
}