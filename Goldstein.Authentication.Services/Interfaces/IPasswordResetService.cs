using Goldstein.Authentication.Domain;

namespace Goldstein.Authentication.Services.Interfaces
{
    public interface IPasswordResetService
    {
        void SendPasswordReset(IdentityUser user);
        bool ValidatePasswordResetRequest(string username, string resetToken);
        void ResetPassword(string username, string password);
    }
}
