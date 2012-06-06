using System;
using System.Linq;
using CrawlAndCollect.Core.Entities.BlockUri;
using CrawlAndCollect.Core.Entities.CrawledUri;
using CrawlAndCollect.Core.Entities.Link;
using CrawlAndCollect.Core.Entities.Log;
using CrawlAndCollect.Core.Extensions;
using CrawlAndCollect.Core.NserviceBus.Messages;
using CrawlAndCollect.Core.Persistence.RavenDB;
using CrawlAndCollect.Core.Persistence.RavenDB.Indexes;
using NServiceBus;
using Raven.Client;

namespace CrawlAndCollect.LinkValidatorEndPoint {
    public class LinkValidatorEndPoint : IHandleMessages<ValidateLinkMessage>, IHandleMessages<RequestUriCrawlMessage>
    {
        private readonly Guid _resourceManagerId = Guid.Parse("0201c8f3-8ffc-4bda-a45d-de54cfc02316");
        private readonly IDocumentSession _session;
        public IBus Bus { get; set; }

        public string[] ValidTopDomains = new[] { "se", "nu" };

        public LinkValidatorEndPoint() {
            _session = DocumentStoreFactory.CreateEndPointStore(_resourceManagerId).OpenSession();
        }

        public void Handle(ValidateLinkMessage message) {
            _session.Store(new Link(message.PageUrl, message.Text, message.Href, message.NoFollow));

            if (ValidateUri(message.Href))
                Bus.Send(new CrawlUrlMessage(message.Href));

            _session.SaveChanges();
        }

        public void Handle(RequestUriCrawlMessage message)
        {
            if(ValidateUri(message.Uri)) {
                _session.Store(new LogRow(DateTime.Now, LogLevel.Information, LogSender.LinkValidatorEndpoint, string.Format("Requested uri accepted ({0})", message.Uri), "The requested uri has been accepted and sent to the crawler!"));
                Bus.Send(new CrawlUrlMessage(message.Uri));
            } else {
                _session.Store(new LogRow(DateTime.Now, LogLevel.Information, LogSender.LinkValidatorEndpoint, string.Format("Requested uri denied ({0})", message.Uri), "The requested uri has been denied and will be discarded!"));
            }

            _session.SaveChanges();
        }

        private bool ValidateUri(Uri uri) {

            // Is an swedish site?
            if (!IsCrawlableTopDomain(uri))
                return false;

            // Is in blocklists?
            if (IsPageBlocked(uri))
                return false;

            // Already crawled this url?
            if (IsPageCrawled(uri))
                return false;

            return true;
        }

        private bool IsPageCrawled(Uri uri)
        {
            return _session.Query<CrawledUri, CrawledUriIndex>().Any(x => x.Uri == uri);
        }

        private bool IsCrawlableTopDomain(Uri uri)
        {
            return ValidTopDomains.Any(validTopDomain => uri.Host.EndsWith(validTopDomain));
        }

        private bool IsPageBlocked(Uri uri)
        {
            var domain = uri.DomainAndTopDomainWitoutWww();
            return _session.Query<BlockUri, BlockedUriIndex>().Any(x => x.Uri == uri) ||
                   _session.Query<BlockDomain, BlockedDomainIndex>().Any(x => x.Domain == domain);
        }
    }
}