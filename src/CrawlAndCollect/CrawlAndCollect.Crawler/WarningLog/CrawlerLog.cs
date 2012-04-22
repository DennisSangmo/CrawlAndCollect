using System.Collections.Generic;

namespace CrawlAndCollect.Crawler.WarningLog {
    public class CrawlerLog {
        private HashSet<CrawlerLogRow> TheLog { get; set; }

        public CrawlerLog() {
            TheLog = new HashSet<CrawlerLogRow>();
        }

        public void Log(string title, string description) {
            TheLog.Add(new CrawlerLogRow(title, description));
        }

        public IEnumerable<CrawlerLogRow> GetLog() {
            return TheLog;
        }
    }
}