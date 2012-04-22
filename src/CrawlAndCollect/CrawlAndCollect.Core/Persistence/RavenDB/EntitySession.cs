using Raven.Client;

namespace CrawlAndCollect.Core.Persistence.RavenDB {
    public class EntitySession {
        public IDocumentSession Db { get; private set; }

        public EntitySession(IDocumentSession db) {
            Db = db;
        }
    }
}