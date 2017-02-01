using System;
using G2WebApp.Core.Data.Contracts;
using Nancy.Cryptography;

namespace G2WebApp.Auth.Core
{
    public class FormsAuthenticationConfiguration
    {
        public string RedirectUrl { get; set; }
        public TimeSpan CookieExpiryDays { get; set; }
        public CryptographyConfiguration CryptographyConfiguration { get; set; }
        public IUserRepository UserRepository { get; set; }
    }
}
