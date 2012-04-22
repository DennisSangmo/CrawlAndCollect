using CrawlAndCollect.Core.NserviceBus.Messages;
using CrawlAndCollect.Core.Persistence.RavenDB;
using CrawlAndCollect.Core.Services;
using NServiceBus;

namespace CrawlAndCollect.LinkValidatorEndPoint {
    public class LinkValidatorEndPoint : IHandleMessages<ValidateLinkMessage> {
        private readonly EntityService _entityService;
        private readonly LogService _logService;
        public IBus Bus { get; set; }

        public LinkValidatorEndPoint() {
            _entityService = SessionFactory.CreateLiveSession();
            _logService = SessionFactory.CreateLogSession();
        }

        public void Handle(ValidateLinkMessage message) {
            throw new System.NotImplementedException();
        }
    }
}