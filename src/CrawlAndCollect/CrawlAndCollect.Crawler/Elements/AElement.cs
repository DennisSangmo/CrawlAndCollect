using System;

namespace CrawlAndCollect.Crawler.Elements {
    public class AElement {
        public string Text { get; set; }
        public Uri Href { get; set; }
        public bool NoFollow { get; set; }

        public AElement(string text, Uri href, bool noFollow) {
            Text = text;
            Href = href;
            NoFollow = noFollow;
        }
    }
}