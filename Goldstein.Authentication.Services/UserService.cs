using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Goldstein.Authentication.Domain;
using Goldstein.Authentication.Infrastructure;
using Goldstein.Authentication.Services.Interfaces;

namespace Goldstein.Authentication.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthenticationConfiguration _configuration;
        private readonly IAuthenticationCookieFactory _cookieFactory;

        public UserService()
        {
        }

        public UserService(IUserRepository userRepository, IConfigurationProvider configurationProvider, IAuthenticationCookieFactory cookieFactory) 
        {
            _userRepository = userRepository;
            _configuration = configurationProvider.GetConfiguration();
            _cookieFactory = cookieFactory;
        }

        public async Task<bool> AuthenticateUserAsync(string userName, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(userName);
            if (ValidateUser(user, password))
            {
                await LoginAsync(user);
                return true;
            }
            user.LoginAttemptCount++;
            if (user.LoginAttemptCount >= _configuration.MaxLoginAttempts)
            {
                user.IsLocked = true;
            }
            await _userRepository.UpdateUserAsync(user);
            return false;
        }
        
        public async Task AddUserAsync(string username, string email, string password)
        {
            var salt = Guid.NewGuid().ToByteArray();
            var passwordHash = password.Hash(salt);
            var identityUser = new IdentityUser(username, email, passwordHash, salt){LastLogin = DateTime.Now};
            await _userRepository.AddUserAsync(identityUser);
        }

        public void Logout()
        {
            HttpContext.Current.Response.SetCookie(_cookieFactory.ExpireAuthenticationCookie());
            //TODO: Mark user as logged out
        }

        public void UnlockUser(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsLoggedInAsync(string username)
        {
            throw new NotImplementedException();
        } 

        private async Task LoginAsync(IdentityUser user)
        {
            user.LoginAttemptCount = 0;
            user.LastLogin = DateTime.Now;
            await _userRepository.UpdateUserAsync(user);
            HttpContext.Current.Response.SetCookie(_cookieFactory.GenerateAuthenticationCookie());
            //TODO: Mark user as logged in
        }

        private static bool ValidateUser(IdentityUser user, string password)
        {
            return !user.IsLocked && password.Hash(user.Salt).SequenceEqual(user.PasswordHash);
        }
    }
}
