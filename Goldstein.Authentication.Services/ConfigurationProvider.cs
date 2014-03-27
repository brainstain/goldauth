using Goldstein.Authentication.Domain;
using Goldstein.Authentication.Infrastructure;
using Goldstein.Authentication.Services.Interfaces;

namespace Goldstein.Authentication.Services
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private readonly AuthenticationConfiguration _authenticationConfiguration;

        public ConfigurationProvider()
        {
        }

        public ConfigurationProvider(IConfiguration configuration)
        {
            _authenticationConfiguration = configuration.GetAuthenticationConfiguration();
        }

        public AuthenticationConfiguration GetConfiguration()
        {
            return _authenticationConfiguration;
        }
    }
}
