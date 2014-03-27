using System.Web;
using Goldstein.Authentication.Domain;
using Goldstein.Authentication.Infrastructure;
using Goldstein.Authentication.Services;
using Goldstein.Authentication.Services.Interfaces;
using Moq;

namespace Goldstein.Authentication.Test
{
    public static class MockBuilder
    {
        public static IConfigurationProvider GetConfigurationProvider()
        {
            var configurationMock = Mock.Of<IConfiguration>(c =>
                                                            c.GetAuthenticationConfiguration() ==
                                                            new AuthenticationConfiguration(5));

            return new ConfigurationProvider(configurationMock);
        } 
        public static IAuthenticationCookieFactory GetAuthenticationCookieFactory()
        {
            return Mock.Of<IAuthenticationCookieFactory>(factory =>
                factory.GenerateAuthenticationCookie() == new HttpCookie("MockCookie") &&
                factory.ExpireAuthenticationCookie() == new HttpCookie("MockCookie"));
        }
    }
}
