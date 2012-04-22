using System;
using NServiceBus;

namespace CrawlAndCollect.Core.NserviceBus.Messages {
    public class ValidateLinkMessage : IMessage {
        public Uri PageUrl { get; set; }
        public string Text { get; set; }
        public Uri Href { get; set; }
        public bool NoFollow { get; set; }

        public ValidateLinkMessage(Uri pageUrl, string text, Uri href, bool noFollow) {
            PageUrl = pageUrl;
            Text = text;
            Href = href;
            NoFollow = noFollow;
        }
    }
}