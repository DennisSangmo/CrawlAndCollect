using System;
using System.Collections.Generic;
using System.Net;
using CrawlAndCollect.Crawler.Collectors;
using CrawlAndCollect.Crawler.Collectors.Interface;
using CrawlAndCollect.Crawler.WarningLog;
using HtmlAgilityPack;

namespace CrawlAndCollect.Crawler {
    /// <summary>
    /// Class used to crawl a site and collect elements using the collectors
    /// </summary>
    public class Crawler
    {
        private readonly Uri _baseUri;
        private WebClient Client { get; set; }
        public CrawlerLog Log { get; private set; }

        private readonly List<ICollector> _collectors;

        public Crawler(Uri baseUri)
        {
            _baseUri = baseUri;
            Client = new WebClient();
            _collectors = new List<ICollector>();
            Log = new CrawlerLog();
        }

        /// <summary>
        /// Add collectors to the crawler
        /// </summary>
        /// <param name="collectors">A collector that will handle and save information from the html document</param>
        public void AddCollectors(params ICollector[] collectors)
        {
            foreach(var collector in collectors) {
                collector.Log = Log;
                _collectors.Add(collector);
            }
        }

        public LinkCollector AddLinkCollector() {
            var lc = new LinkCollector();
            AddCollectors(lc);
            return lc;
        }

        public void Crawl() {
            string html;
            try {
                html = Client.DownloadString(_baseUri);
            }
            catch (WebException e) {
                Log.Log("Error when downloading url: " + _baseUri, e.Message);
                throw e;
            }

            var doc = new HtmlDocument
                      {
                          OptionAddDebuggingAttributes = false,
                          OptionAutoCloseOnEnd = true,
                          OptionFixNestedTags = true,
                          OptionReadEncoding = true
                      };

            doc.LoadHtml(html);

            // Call custom collectors
            foreach (var collector in _collectors) {
                collector.Collect(_baseUri, doc);
            }
        }
    }
}