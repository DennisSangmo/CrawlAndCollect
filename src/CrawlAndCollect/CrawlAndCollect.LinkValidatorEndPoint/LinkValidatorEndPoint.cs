using System;
using CrawlAndCollect.Core.Bs;
using CrawlAndCollect.Core.Bs.LinkValidator;
using CrawlAndCollect.Core.Entities.Log;
using CrawlAndCollect.Core.NserviceBus.Messages;
using CrawlAndCollect.Core.Persistence.RavenDB;
using NServiceBus;

namespace CrawlAndCollect.LinkValidatorEndPoint {
    public class LinkValidatorEndPoint : IHandleMessages<ValidateLinkMessage> {
        private readonly LinkValidator _linkValidator;
        public IBus Bus { get; set; }

        public LinkValidatorEndPoint() {
            var es = SessionFactory.CreateLiveSession();
            var ls = SessionFactory.CreateLogSession();
            _linkValidator = new LinkValidator(es, ls);
        }

        public void Handle(ValidateLinkMessage message) {
            if(_linkValidator.ContinueCrawling(message.PageUrl, message.Text, message.Href, message.NoFollow))
                Bus.Send(new CrawlUrlMessage(message.Href));
        }
    }
}