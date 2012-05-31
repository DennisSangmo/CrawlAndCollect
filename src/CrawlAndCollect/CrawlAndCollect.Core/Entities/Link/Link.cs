using System;
using CrawlAndCollect.Core.Entities.Base;

namespace CrawlAndCollect.Core.Entities.Link {
    public class Link : Entity{
        public Guid PageId { get; set; }
        public string Text { get; set; }
        public Uri Href { get; set; }
        public bool NoFollow { get; set; }

        public Link(Guid pageId, string text, Uri href, bool noFollow) {
            PageId = pageId;
            Text = text;
            Href = href;
            NoFollow = noFollow;
        }
    }
}