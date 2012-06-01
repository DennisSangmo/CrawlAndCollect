using CrawlAndCollect.Core.Persistence.RavenDB;
using CrawlAndCollect.Core.Services;
using Raven.Client;
using StructureMap.Configuration.DSL;

namespace CrawlAndCollect.Core.IoC.Registries {
    public class CrawlAndCollectRegistry : Registry {
        public CrawlAndCollectRegistry() {
            For<IDocumentStore>()
                .Singleton().Use(DocumentStoreFactory.CreateLiveStore);
            For<IDocumentSession>()
                .Use(cfg => cfg.GetInstance<IDocumentStore>().OpenSession());
            For<EntityService>().Singleton()
             .Use(cfg => new EntityService(cfg.GetInstance<IDocumentSession>()));
        }
    }
}