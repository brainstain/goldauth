using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Goldstein.Authentication.Infrastructure;
using Goldstein.Authentication.Services;
using Goldstein.Authentication.Services.Interfaces;

namespace Goldstein.Authentication.Installer
{
    public static class Installer
    {
        public static WindsorContainer Install()
        {
            var container = new WindsorContainer();
           //todo REGISTER MY STUFF
            container.Register(Component.For(typeof (IUserRepository)).ImplementedBy(typeof(UserRepository)));
            container.Register(Component.For(typeof (IUserService)).ImplementedBy(typeof(UserService)));
            container.Register(Component.For(typeof (IConfigurationProvider)).ImplementedBy(typeof (ConfigurationProvider)));
            container.Register(Component.For(typeof(IAuthenticationCookieFactory)).ImplementedBy(typeof(FormsAuthenticationCookieFactory)));
            container.Register(Component.For<IControllerFactory>().ImplementedBy<ControllerFactory>().LifeStyle.Singleton);
            return container;
        }
    }
}
