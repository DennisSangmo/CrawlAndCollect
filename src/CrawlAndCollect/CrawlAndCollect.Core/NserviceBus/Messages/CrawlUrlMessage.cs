using System;
using NServiceBus;

namespace CrawlAndCollect.Core.NserviceBus.Messages {
    public class CrawlUrlMessage : IMessage{
        public Uri PageUrl { get; set; }

        public CrawlUrlMessage(Uri uri) {
            PageUrl = uri;
        }
    }
}