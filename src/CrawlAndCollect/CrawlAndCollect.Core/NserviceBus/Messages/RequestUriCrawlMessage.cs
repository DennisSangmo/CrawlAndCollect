using System;
using NServiceBus;

namespace CrawlAndCollect.Core.NserviceBus.Messages {
    public class RequestUriCrawlMessage : IMessage {
        public Uri Uri { get; set; }

        public RequestUriCrawlMessage(Uri uri) {
            Uri = uri;
        }
    }
}