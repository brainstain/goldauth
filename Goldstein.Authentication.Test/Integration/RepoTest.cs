using Castle.Windsor;
using Goldstein.Authentication.Infrastructure;
using Goldstein.Authentication.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Goldstein.Authentication.Test.Integration
{
    [TestClass]
    public class RepoTest
    {
        private readonly IUserRepository _repository;
        private readonly IWindsorContainer _container;
        public RepoTest()
        {
            _repository = new UserRepository();
            _container = Installer.Installer.Install();
        }

        //[TestMethod]
        //public void AddUserAsync()
        //{
        //    var userService = _container.Resolve<IUserService>();
        //    userService.AddUserAsync("michael", "michael@email.com", "password");
            
        //}

        //[TestMethod]
        //public void GetByUserName()
        //{
        //    //TODO: Add a user if doen't exist, get user, delete user if didnt exist
        //    var user = _repository.GetUserByUsernameAsync("bobby").Result;
        //    Assert.IsNotNull(user);
        //}
    }
}
