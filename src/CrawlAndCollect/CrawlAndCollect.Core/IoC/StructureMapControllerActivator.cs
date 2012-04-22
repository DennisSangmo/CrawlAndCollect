using System;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace CrawlAndCollect.Core.IoC {
    public class StructureMapControllerActivator : IControllerActivator {
        
        public StructureMapControllerActivator(IContainer container)
        {
            _container = container;
        }
 
        private readonly IContainer _container;
 
        public IController Create(RequestContext requestContext, Type controllerType)
        {
            return _container.GetInstance(controllerType) as IController;
        }
    }
}