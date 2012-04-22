using System;
using CrawlAndCollect.Core.Entities.Base;

namespace CrawlAndCollect.Core.Entities.PageUrl {
    public class PageUrl : Entity {
        public Uri Uri { get; set; }

        public PageUrl() {}

        public PageUrl(Uri uri){
            Uri = uri;
        }
    }
}