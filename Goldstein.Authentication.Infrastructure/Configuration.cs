using Goldstein.Authentication.Domain;

namespace Goldstein.Authentication.Infrastructure
{
    public class Configuration : IConfiguration
    {
        //TODO Get values from config file
        public AuthenticationConfiguration GetAuthenticationConfiguration()
        {
            return new AuthenticationConfiguration(5);
        }
    }
}
