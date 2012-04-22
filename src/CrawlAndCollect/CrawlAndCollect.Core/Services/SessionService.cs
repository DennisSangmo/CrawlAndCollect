using Raven.Client;

namespace CrawlAndCollect.Core.Services {
    public abstract class SessionService {
        public IDocumentSession Session { get; protected set; }

        public void Save() {
            Session.SaveChanges();
        }
    }
}