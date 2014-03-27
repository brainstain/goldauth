using System;
using Goldstein.Authentication.Services;
using Goldstein.Authentication.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Goldstein.Authentication.Test.UnitTests
{
    [TestClass]
    public class CookieFactoryTest
    {
        private readonly IAuthenticationCookieFactory _cookieFactory;

        public CookieFactoryTest()
        {
            _cookieFactory = new FormsAuthenticationCookieFactory();
        }

        [TestMethod]
        public void GenerateAuthentication()
        {
            var cookie = _cookieFactory.GenerateAuthenticationCookie();
            Assert.IsTrue(cookie.HttpOnly);
            Assert.IsTrue(cookie.Expires > DateTime.Now);
        }

        [TestMethod]
        public void ExpireAuthentication()
        {
            Assert.IsTrue(_cookieFactory.ExpireAuthenticationCookie().Expires < DateTime.Now);
        }
    }
}
