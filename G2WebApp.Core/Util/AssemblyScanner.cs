using System;
using System.Collections.Generic;
using System.Linq;

namespace G2WebApp.Core.Util
{
    public static class AssemblyScanner
    {
        public static IEnumerable<Type> TypesOf(Type desiredType)
        {
            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(desiredType.IsAssignableFrom);
        }
    }
}
