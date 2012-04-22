using CrawlAndCollect.Core.Services;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;

namespace CrawlAndCollect.Core.Persistence.RavenDB {
    public static class SessionFactory {
        public static IDocumentSession CreateInMemorySession()
        {
            var ds = new EmbeddableDocumentStore { RunInMemory = true, };
            ds.Initialize();
            return ds.OpenSession();
        }

        public static EntityService CreateLiveSession() {
            var ds = new DocumentStore { ConnectionStringName = "RavenDBEnties" };
            ds.Initialize();
            return new EntityService(ds.OpenSession());
        } 

        public static LogService CreateLogSession() {
            var ds = new DocumentStore { ConnectionStringName = "RavenDBLog" };
            ds.Initialize();
            return new LogService(ds.OpenSession());
        } 
    }
}