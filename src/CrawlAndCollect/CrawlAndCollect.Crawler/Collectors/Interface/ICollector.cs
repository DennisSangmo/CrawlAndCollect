using System;
using CrawlAndCollect.Crawler.WarningLog;
using HtmlAgilityPack;

namespace CrawlAndCollect.Crawler.Collectors.Interface {
    /// <summary>
    /// Collector interface
    /// </summary>
    public interface ICollector {
        /// <summary>
        /// The actuall method called by the crawlen when its time to collect
        /// </summary>
        /// <param name="baseUri">The baseuri of the site being crawled. Used for complimenting relativ urls</param>
        /// <param name="doc">The actuall HTML-document</param>
        void Collect(Uri baseUri, HtmlDocument doc);

        CrawlerLog Log { get; set; }
    }
}