using System;
using Nancy.Cookies;
using Nancy.Cryptography;

namespace G2WebApp.Auth.Core
{
    public static class CookieBuilder
    {
        public static INancyCookie BuildCookie(string name, string value, DateTime expDate, IEncryptionProvider provider)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));

            var cookieValue = provider.Encrypt(value);
            var cookie = new NancyCookie(name, cookieValue, expDate);

            return cookie;
        }

        public static INancyCookie BuildLogOutCookie(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            var cookie = new NancyCookie(name, string.Empty, DateTime.Now.AddDays(-1));
            return cookie;
        }
    }
}
