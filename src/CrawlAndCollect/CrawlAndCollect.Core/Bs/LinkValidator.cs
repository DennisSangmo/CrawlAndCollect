using System;
using System.Linq;
using CrawlAndCollect.Core.Entities.BlockUri;
using CrawlAndCollect.Core.Entities.CrawledUri;
using CrawlAndCollect.Core.Extensions;
using CrawlAndCollect.Core.Persistence.RavenDB.Indexes;
using Raven.Client;

namespace CrawlAndCollect.Core.Bs {
    public class LinkValidator {
        private IDocumentSession Session { get; set; }

        public string[] ValidTopDomains = new[] { "se", "nu" };

        public LinkValidator(IDocumentSession session) {
            Session = session;
        }

        /// <summary>
        /// Validate link! Determens if the page behind the uri should be crawled
        /// </summary>
        public bool Validate(Uri uri) {
            return IsCrawlableTopDomain(uri) && // Is an swedish site?
                   !IsUriBlocked(uri) && // Is in blocklists?
                   !IsPageCrawled(uri); // Already crawled this url?
        }

        /// <summary>
        /// Validates if the page behind the uri already is crawled
        /// </summary>
        private bool IsPageCrawled(Uri uri) {
            return Session.Query<CrawledUri, CrawledUriIndex>().Any(x => x.Uri == uri);
        }

        /// <summary>
        /// Validates if the uri is of topdomain .se or .nu
        /// </summary>
        private bool IsCrawlableTopDomain(Uri uri) {
            return ValidTopDomains.Any(validTopDomain => uri.Host.EndsWith(validTopDomain));
        }

        /// <summary>
        /// Validates if the page behind the uri is manually blocked
        /// </summary>
        private bool IsUriBlocked(Uri uri) {
            var domain = uri.DomainAndTopDomainWitoutWww();
            return Session.Query<BlockUri, BlockedUriIndex>().Any(x => x.Uri == uri) ||
                   Session.Query<BlockDomain, BlockedDomainIndex>().Any(x => x.Domain == domain);
        }
    }
}