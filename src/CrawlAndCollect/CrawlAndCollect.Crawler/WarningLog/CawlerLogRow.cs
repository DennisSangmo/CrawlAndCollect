using System;

namespace CrawlAndCollect.Crawler.WarningLog {
    public class CrawlerLogRow {
        public DateTime Raised { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public CrawlerLogRow(string title, string description){
            Raised = DateTime.Now;
            Title = title;
            Description = description;
        }
    }
}