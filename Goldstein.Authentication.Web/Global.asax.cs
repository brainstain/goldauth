using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Goldstein.Authentication.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer container;

        protected void Application_Start()
        {
            container = Installer.Installer.Install();
            container.Register(Classes.FromThisAssembly().BasedOn<Controller>().LifestyleTransient());
            container.Register(Component.For<IWindsorContainer>().Instance(container));


            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterControllerFactory();
        }

        private static void RegisterControllerFactory() 
        {  
              var controllerFactory = container.Resolve<IControllerFactory>();
              ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }

}
