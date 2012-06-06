using System;
using CrawlAndCollect.Core.Entities.Base;

namespace CrawlAndCollect.Core.Entities.CrawledUri {
    public class CrawledUri : Entity{
        public DateTime Crawled { get; set; }
        public Uri Uri { get; set; }
        
        public CrawledUri(Uri uri) {
            Crawled = DateTime.Now;
            Uri = uri;
        }
    }
}