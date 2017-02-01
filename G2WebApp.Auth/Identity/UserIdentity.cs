using System.Collections.Generic;
using Nancy.Security;

namespace G2WebApp.Auth.Identity
{
    public class UserIdentity : IUserIdentity
    {
        public string UserName { get; set; }
        public IEnumerable<string> Claims { get; set; }
    }
}
