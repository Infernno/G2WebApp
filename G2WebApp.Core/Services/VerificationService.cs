using G2WebApp.Core.Data.Contracts;
using G2WebApp.Core.Data.Entities;
using G2WebApp.Core.Data.Repositories;

namespace G2WebApp.Core.Services
{
    public class VerificationService
    {
        private IUserRepository userRepository = new UserRepository();

        private static VerificationService instance;
        private static object syncLock = new object();

        public static VerificationService Current
        {
            get
            {
                if (instance == null)
                {
                    lock (syncLock)
                    {
                        instance = new VerificationService();
                    }
                }

                return instance;
            }
        }

        private VerificationService() { }

        public bool isUserBanned(string ip)
        {
            var user = userRepository.Find(u => u.Ip == ip);

            if (user == null)
                return false;

            return user.Role == UserGroup.Banned;
        }
    }
}
