using System.Web.Mvc;
using CrawlAndCollect.Core.IoC.Registries;
using CrawlAndCollect.Core.NserviceBus.UnitOfWork;
using CrawlAndCollect.Core.Persistence.RavenDB;
using NServiceBus;
using NServiceBus.Persistence.Raven;
using NServiceBus.UnitOfWork;
using StructureMap;

namespace CrawlAndCollect.Core.IoC {
    public static class CrawlAndCollectContainer {
        private static IContainer _webContainer;
        private static IContainer _epContainer;

        public static IContainer GetWebContainer() {
            var bus = Configure.WithWeb()
                    .DefineEndpointName("CrawlAndCollect")
                    .DefaultBuilder()
                    .XmlSerializer()
                    .Log4Net()
                    .MsmqTransport()
                    .UnicastBus()
                    .CreateBus()
                    .Start( () => Configure.Instance.ForInstallationOn<NServiceBus.Installation.Environments.Windows>().Install());
            return _webContainer ?? (_webContainer = new Container(x => {
                                                                           x.For<IControllerActivator>()
                                                                            .Use<StructureMapControllerActivator>();
                                                                           x.For<IBus>().Singleton()
                                                                            .Use(bus);
                                                                           x.AddRegistry<CrawlAndCollectRegistry>();
                                                                        }));
        }

        public static IContainer GetEndPointContainer()
        {
            return _epContainer ?? (_epContainer = new Container(x =>
            {
                x.AddRegistry<CrawlAndCollectRegistry>();
                x.For<IManageUnitsOfWork>()
                    .Use<MyRavenUnitOfWork>();
            }));
        }
    }
}