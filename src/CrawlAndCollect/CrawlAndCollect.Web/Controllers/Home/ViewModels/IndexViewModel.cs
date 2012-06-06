using CrawlAndCollect.Core.Entities.CrawledUri;

namespace CrawlAndCollect.Web.Controllers.Home.ViewModels {
    public class IndexViewModel {
        public CrawledUri LastCrawled { get; set; }
        public int Pages { get; set; }
        public int Links { get; set; }

        public IndexViewModel(CrawledUri lastCrawled, int pages, int links) {
            LastCrawled = lastCrawled;
            Pages = pages;
            Links = links;
        }
    }
}