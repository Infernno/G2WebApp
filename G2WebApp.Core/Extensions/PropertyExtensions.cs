using System;
using System.Linq.Expressions;

namespace G2WebApp.Core.Extensions
{
    public static class PropertyExtensions
    {
        public static T SetValue<T>(this T target, Expression<Func<T, object>> memberLamda, object value)
        {
            throw new NotImplementedException();
        }
    }
}
