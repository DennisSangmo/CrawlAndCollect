using System;
using System.Linq;
using System.Transactions;
using CrawlAndCollect.Core.Entities.CrawledUri;
using CrawlAndCollect.Core.Extensions;
using CrawlAndCollect.Core.Persistence.RavenDB;
using NServiceBus;
using CrawlAndCollect.Core.Entities.Log;
using CrawlAndCollect.Core.NserviceBus.Messages;
using CrawlAndCollect.Core.Services;
using Raven.Client;

namespace CrawlAndCollect.CrawlerEndPoint
{
    public class CrawlerEndPoint : IHandleMessages<CrawlUrlMessage>
    {
        private readonly Guid _resourceManagerId = Guid.Parse("14270941-a50b-42cb-ad70-3726e15ddfbc");
        private readonly IDocumentSession _session;
        public IBus Bus { get; set; }

        public CrawlerEndPoint()
        {
            _session = DocumentStoreFactory.CreateEndPointStore(_resourceManagerId).OpenSession();
        }

        public void Handle(CrawlUrlMessage message) {
            var crawler = new Crawler.Crawler(message.PageUrl);
            var linkCollector = crawler.AddLinkCollector();
            crawler.Crawl();

            // Save Crawlerlog to common log
            var log = crawler.Log.GetLog().Select(x => new LogRow(x.Raised, LogLevel.Warning, LogSender.CrawlerEndPoint, x.Title, x.Description));
            log.Each(_session.Store);

            // Send link for validation
            var links = linkCollector.AllLinks.Select(x => new ValidateLinkMessage(message.PageUrl, x.Text, x.Href, x.NoFollow));
            links.Each(x => Bus.Send(x));

            _session.Store(new CrawledUri(message.PageUrl));

            _session.SaveChanges();
        }
    }
}
