using System.Linq;
using System.Transactions;
using CrawlAndCollect.Core.Extensions;
using NServiceBus;
using CrawlAndCollect.Core.Entities.Log;
using CrawlAndCollect.Core.NserviceBus.Messages;
using CrawlAndCollect.Core.Services;

namespace CrawlAndCollect.CrawlerEndPoint
{
    public class CrawlerEndPoint : IHandleMessages<CrawlUrlMessage>
    {
        private readonly EntityService _entityService;
        public IBus Bus { get; set; }

        public CrawlerEndPoint(EntityService entityService) {
            _entityService = entityService;
        }

        public void Handle(CrawlUrlMessage message) {
            var crawler = new Crawler.Crawler(message.PageUrl);
            var linkCollector = crawler.AddLinkCollector();
            crawler.Crawl();

            using (var transaction = new TransactionScope()) {
                // Save Crawlerlog to common log
                var log = crawler.Log.GetLog().Select(x => new LogRow(x.Raised, LogLevel.Warning, LogSender.CrawlerEndPoint, x.Title, x.Description));
                _entityService.StoreLog(log);

                // Send link for validation
                var links = linkCollector.AllLinks.Select(x => new ValidateLinkMessage(message.PageUrl, x.Text, x.Href, x.NoFollow));
                links.Each(x => Bus.Send(x));

                _entityService.AddCrawledUri(message.PageUrl);
                transaction.Complete();
            }
        }
    }
}
