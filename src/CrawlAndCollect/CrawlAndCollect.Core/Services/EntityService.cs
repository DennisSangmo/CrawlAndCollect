using System;
using System.Collections.Generic;
using CrawlAndCollect.Core.Entities.BlockUri;
using CrawlAndCollect.Core.Entities.CrawledUri;
using CrawlAndCollect.Core.Entities.Link;
using CrawlAndCollect.Core.Entities.Log;
using CrawlAndCollect.Core.Persistence.RavenDB.Indexes;
using Raven.Client;
using System.Linq;
using CrawlAndCollect.Core.Extensions;

namespace CrawlAndCollect.Core.Services {
    public class EntityService {
        public IDocumentSession Session { get; set; }
        public EntityService(IDocumentSession session) {
            Session = session;
        }

        public Link StoreLink(Uri page, string text, Uri href, bool noFollow) {
            var link = new Link(page, text, href, noFollow);
            Session.Store(link);
            return link;
        }

        public bool IsPageBlocked(Uri uri) {
            var domain = uri.DomainAndTopDomainWitoutWww();
            return Session.Query<BlockUri, BlockedUriIndex>().Any(x => x.Uri == uri) || 
                   Session.Query<BlockDomain, BlockedDomainIndex>().Any(x => x.Domain == domain);
        }

        public void SaveBlockUrl(BlockUri blockUri) {
            Session.Store(blockUri);
        }

        public bool IsPageCrawled(Uri linkHref) {
            return Session.Query<CrawledUri, CrawledUriIndex>().Any(x => x.Uri == linkHref);
        }

        public void AddCrawledUri(Uri uri) {
            Session.Store(new CrawledUri(uri));
        }

        public void StoreLog(IEnumerable<LogRow> log)
        {
            log.Each(Session.Store);
        }

        public IEnumerable<LogRow> GetLog()
        {
            return Session.Query<LogRow>();
        }
    }
}