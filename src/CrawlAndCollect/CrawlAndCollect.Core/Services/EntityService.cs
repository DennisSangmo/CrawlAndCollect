using Raven.Client;

namespace CrawlAndCollect.Core.Services {
    public class EntityService : SessionService {
        public EntityService(IDocumentSession session) {
            Session = session;
        }
    }
}