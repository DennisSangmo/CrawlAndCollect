using System;
using CrawlAndCollect.Core.Bs;
using CrawlAndCollect.Core.Entities.Link;
using CrawlAndCollect.Core.Entities.Log;
using CrawlAndCollect.Core.NserviceBus.Messages;
using CrawlAndCollect.Core.Persistence.RavenDB;
using NServiceBus;
using Raven.Client;

namespace CrawlAndCollect.LinkValidatorEndPoint {
    public class LinkValidatorEndPoint : IHandleMessages<ValidateLinkMessage>, IHandleMessages<RequestUriCrawlMessage>
    {
        private readonly Guid _resourceManagerId = Guid.Parse("0201c8f3-8ffc-4bda-a45d-de54cfc02316");
        private readonly IDocumentSession _session;
        private readonly LinkValidator _validator;
        public IBus Bus { get; set; }

        public LinkValidatorEndPoint() {
            _session = DocumentStoreFactory.CreateEndPointStore(_resourceManagerId).OpenSession();
            _validator = new LinkValidator(_session);
        }

        public void Handle(ValidateLinkMessage message) {
            _session.Store(new Link(message.PageUrl, message.Text, message.Href, message.NoFollow));

            if (_validator.Validate(message.Href))
                Bus.Send(new CrawlUrlMessage(message.Href));

            _session.SaveChanges();
        }

        public void Handle(RequestUriCrawlMessage message)
        {
            if (_validator.Validate(message.Uri))
            {
                _session.Store(new LogRow(DateTime.Now, LogLevel.Information, LogSender.LinkValidatorEndpoint, string.Format("Requested uri accepted ({0})", message.Uri), "The requested uri has been accepted and sent to the crawler!"));
                Bus.Send(new CrawlUrlMessage(message.Uri));
            } else {
                _session.Store(new LogRow(DateTime.Now, LogLevel.Information, LogSender.LinkValidatorEndpoint, string.Format("Requested uri denied ({0})", message.Uri), "The requested uri has been denied and will be discarded!"));
            }

            _session.SaveChanges();
        }
    }
}