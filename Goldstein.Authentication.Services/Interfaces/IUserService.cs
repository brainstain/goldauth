using System.Threading.Tasks;

namespace Goldstein.Authentication.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> AuthenticateUserAsync(string userName, string password);
        Task AddUserAsync(string username, string email, string password);
        void Logout();
        void UnlockUser(string username);
        Task<bool> IsLoggedInAsync(string userName);
    }
}
