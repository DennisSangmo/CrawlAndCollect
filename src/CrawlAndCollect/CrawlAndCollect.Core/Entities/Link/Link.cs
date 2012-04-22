using System;
using CrawlAndCollect.Core.Entities.Base;

namespace CrawlAndCollect.Core.Entities.Link {
    public class Link : Entity{
        public string Text { get; set; }
        public Uri Href { get; set; }
        public bool NoFollow { get; set; }

        public Link(string text, Uri href, bool noFollow) {
            Text = text;
            Href = href;
            NoFollow = noFollow;
        }
    }
}