namespace Goldstein.Authentication.Domain
{
    public class AuthenticationConfiguration
    {
        public AuthenticationConfiguration(int maxLoginAttempts)
        {
            MaxLoginAttempts = maxLoginAttempts;
        }

        public int MaxLoginAttempts { get; private set; }
    }
}
