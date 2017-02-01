using System;
using NLog;

namespace G2WebApp.Core.Util
{
    public static class Debug
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static Debug()
        {
            AppDomain.CurrentDomain.UnhandledException += HandleCrash;
        }

        public static void Log(object message)
        {
            logger.Info(message);
        }

        public static void Log(object message, params object[] args)
        {
            logger.Info(message.ToString(), args);
        }

        public static void LogWarning(object message)
        {
            logger.Warn(message);
        }

        public static void LogWarning(object message, params object[] args)
        {
            logger.Warn(message.ToString(), args);
        }

        public static void LogError(object message)
        {
            logger.Error(message);
        }

        public static void LogError(object message, params object[] args)
        {
            logger.Error(message.ToString(), args);
        }

        private static void HandleCrash(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception) e.ExceptionObject;

            logger.Fatal("***CRASH***");
            logger.Fatal(ex);
            logger.Fatal("***END***");
        }
    }
}
