using System;
using CrawlAndCollect.Core.Entities.BlockUri;
using CrawlAndCollect.Core.Entities.CrawledUri;
using CrawlAndCollect.Core.Entities.Link;
using CrawlAndCollect.Core.Persistence.RavenDB.Indexes;
using Raven.Client;
using System.Linq;
using CrawlAndCollect.Core.Extensions;

namespace CrawlAndCollect.Core.Services {
    public class EntityService : SessionService {
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
    }
}