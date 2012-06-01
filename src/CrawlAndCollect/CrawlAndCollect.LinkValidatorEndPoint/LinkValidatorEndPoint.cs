using System;
using System.Linq;
using System.Transactions;
using CrawlAndCollect.Core.NserviceBus.Messages;
using CrawlAndCollect.Core.Services;
using NServiceBus;

namespace CrawlAndCollect.LinkValidatorEndPoint {
    public class LinkValidatorEndPoint : IHandleMessages<ValidateLinkMessage> {
        private readonly EntityService _entityService;
        public IBus Bus { get; set; }

        public string[] ValidTopDomains = new[] { "se", "nu" };

        public LinkValidatorEndPoint(EntityService entityService) {
            _entityService = entityService;
        }

        public void Handle(ValidateLinkMessage message) {
            using (var transaction = new TransactionScope()) {
                _entityService.StoreLink(message.PageUrl, message.Text, message.Href, message.NoFollow);

                if (ValidateUri(message.Href))
                    Bus.Send(new CrawlUrlMessage(message.Href));

                transaction.Complete();
            }
        }

        private bool ValidateUri(Uri uri) {

            // Is an swedish site?
            if (!IsCrawlableTopDomain(uri))
                return false;

            // Is in blocklists?
            if (_entityService.IsPageBlocked(uri))
                return false;

            // Already crawled this url?
            if (_entityService.IsPageCrawled(uri))
                return false;

            return true;
        }

        private bool IsCrawlableTopDomain(Uri uri)
        {
            return ValidTopDomains.Any(validTopDomain => uri.Host.EndsWith(validTopDomain));
        }
    }
}