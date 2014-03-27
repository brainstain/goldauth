using Goldstein.Authentication.Domain;

namespace Goldstein.Authentication.Services.Interfaces
{
    public interface IConfigurationProvider
    {
        AuthenticationConfiguration GetConfiguration();
    }
}
