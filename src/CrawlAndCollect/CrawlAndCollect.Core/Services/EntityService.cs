using System;
using CrawlAndCollect.Core.Entities.BlockUri;
using CrawlAndCollect.Core.Entities.Link;
using CrawlAndCollect.Core.Entities.Page;
using Raven.Client;
using System.Linq;
using CrawlAndCollect.Core.Extensions;

namespace CrawlAndCollect.Core.Services {
    public class EntityService : SessionService {
        public EntityService(IDocumentSession session) {
            Session = session;
        }

        public Page GetPage(Uri pageUri) {
            var page = Session.Query<Page>().SingleOrDefault(x => x.Uri == pageUri);
            if (page != null) {
                page.Links = Session.Query<Link>().Where(x => x.PageId == page.Id);
            }
            return page;
        }

        public Link StoreLink(Guid pageId, string text, Uri href, bool noFollow) {
            var link = new Link(pageId, text, href, noFollow);
            Session.Store(link);
            return link;
        }

        public Page StorePage(Uri pageUrl) {
            var page = new Page(pageUrl);
            Session.Store(page);
            return page;
        }

        public bool IsUriBlocked(Uri uri) {
            var domain = uri.DomainAndTopDomainWitoutWww();
            return Session.Query<BlockUri>().Any(x => x.Uri == uri) || 
                   Session.Query<BlockDomain>().Any(x => x.Domain == domain);
        }

        public void SaveBlockUrl(BlockUri blockUri) {
            Session.Store(blockUri);
        }
    }
}