using Goldstein.Authentication.Domain;

namespace Goldstein.Authentication.Infrastructure
{
    public interface IConfiguration
    {
        AuthenticationConfiguration GetAuthenticationConfiguration(); 
    }
}
