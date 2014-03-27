using System;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;

namespace Goldstein.Authentication.Services
{
    public class ControllerFactory : DefaultControllerFactory
    {
        readonly IWindsorContainer container;

        public ControllerFactory(IWindsorContainer container)
        {
            this.container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return container.Resolve(controllerType) as IController;
        }

        public override void ReleaseController(IController controller)
        {
            container.Release(controller);
        }
    }
}
