using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Goldstein.Authentication.Domain;
using Goldstein.Authentication.Infrastructure;
using Goldstein.Authentication.Services;
using Goldstein.Authentication.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Goldstein.Authentication.Test.UnitTests
{
    [TestClass]
    public class UserServiceTest
    {
        private readonly IdentityUser _user;
        private readonly IUserService _userService;
        private readonly Mock<IUserRepository> _repository;

        const string Password = "mypassword";
        public UserServiceTest()
        {
            HttpContext.Current = new HttpContext(new HttpRequest(null, "http://tempuri.org", null), new HttpResponse(null));

            var salt = Guid.NewGuid().ToByteArray();
            _user = new IdentityUser("MyUser", "email", Password.Hash(salt), salt);
            _repository = new Mock<IUserRepository>();
            
            _repository.Setup(r => r.UpdateUserAsync(It.IsAny<IdentityUser>())).Returns(Task.Run(() => { }));
            _repository.Setup(r => r.GetUserByUsernameAsync("MyUser")).ReturnsAsync(_user);
            _userService = new UserService(_repository.Object, MockBuilder.GetConfigurationProvider(),
                                              MockBuilder.GetAuthenticationCookieFactory());
        }

        [TestMethod]
        public async Task AuthenticateUser()
        {
            var test = await _userService.AuthenticateUserAsync("MyUser", Password);
            Assert.IsTrue(test);
        }

        [TestMethod]
        public void LogUserOut()
        {
            _userService.Logout();
            Assert.IsTrue(HttpContext.Current.Response.Cookies[FormsAuthentication.FormsCookieName].Expires < DateTime.Now);
        }

        [TestMethod]
        public async Task AddUser()
        {
            _repository.Setup(r => r.AddUserAsync(_user));
            await _userService.AddUserAsync(_user.UserName, _user.Email, Password);
            _repository.Verify(c => c.AddUserAsync(It.Is<IdentityUser>(addedUser => 
                addedUser.UserName.Equals( _user.UserName) &&
                addedUser.Email.Equals(_user.Email) && 
                Password.Hash(_user.Salt).SequenceEqual(_user.PasswordHash))), Times.Once());
        }
    }
}
