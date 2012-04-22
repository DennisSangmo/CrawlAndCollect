using System;
using CrawlAndCollect.Core.Entities.PageUrl;
using NServiceBus;

namespace CrawlAndCollect.Core.NserviceBus.Messages {
    public class CrawlUrlMessage : IMessage{
        public Uri PageUrl { get; set; }

        public CrawlUrlMessage(PageUrl pageUrl) {
            PageUrl = pageUrl.Uri;
        }
    }
}