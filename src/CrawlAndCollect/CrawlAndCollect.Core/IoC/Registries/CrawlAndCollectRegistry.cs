using CrawlAndCollect.Core.Persistence.RavenDB;
using CrawlAndCollect.Core.Services;
using StructureMap.Configuration.DSL;

namespace CrawlAndCollect.Core.IoC.Registries {
    public class CrawlAndCollectRegistry : Registry {
        public CrawlAndCollectRegistry() {
            For<EntityService>().Singleton()
             .Use(SessionFactory.CreateLiveSession);
            For<LogService>().Singleton()
             .Use(SessionFactory.CreateLogSession);
        }
    }
}