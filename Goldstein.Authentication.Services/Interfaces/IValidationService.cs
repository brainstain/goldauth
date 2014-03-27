namespace Goldstein.Authentication.Services.Interfaces
{
    public interface IValidationService
    {
        bool ValidatePassword(string password);
        bool ValidateUsername(string username);
        bool ValidateEmail(string email);
    }
}
