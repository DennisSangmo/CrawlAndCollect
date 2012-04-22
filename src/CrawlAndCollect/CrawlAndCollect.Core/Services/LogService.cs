using System.Collections.Generic;
using CrawlAndCollect.Core.Entities.Log;
using CrawlAndCollect.Core.Extensions;
using Raven.Client;

namespace CrawlAndCollect.Core.Services {
    public class LogService : SessionService {

        public LogService(IDocumentSession session)
        {
            Session = session;
        }

        public void Store(IEnumerable<LogRow> log) {
            log.Each(Session.Store);
        }

        public IEnumerable<LogRow> GetAll()
        {
            return Session.Query<LogRow>();
        }
    }
}