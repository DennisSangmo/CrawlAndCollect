using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StructureMap;

namespace CrawlAndCollect.Core.IoC {
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        private readonly IContainer _container;

        public StructureMapDependencyResolver(IContainer container) {
            _container = container;
        }

        public object GetService(Type serviceType) {
            var instance = _container.TryGetInstance(serviceType);

            if (instance == null && !serviceType.IsAbstract)
            {
                _container.Configure(c => c.AddType(serviceType, serviceType));
                instance = _container.TryGetInstance(serviceType);
            }

            return instance;
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }
    }
}