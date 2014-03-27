using System.Threading.Tasks;
using Goldstein.Authentication.Domain;

namespace Goldstein.Authentication.Infrastructure
{
    public interface IUserRepository
    {
        Task<IdentityUser> GetUserByUsernameAsync(string username);
        Task UpdateUserAsync(IdentityUser user);
        Task AddUserAsync(IdentityUser user);
        Task SetUserForPasswordResetAsync(string username, char[] resetToken);
        Task<char[]> GetPasswordResetTokenAsync(string username);
    }
}
