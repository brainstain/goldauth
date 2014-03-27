using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Goldstein.Authentication.Test.UnitTests
{
    [TestClass]
    public class ConfigurationTest
    {
        [TestMethod]
        public void ProviderTest()
        {
            var provider = MockBuilder.GetConfigurationProvider();
            Assert.IsTrue(provider.GetConfiguration().MaxLoginAttempts == 5);
        }
    }
}
