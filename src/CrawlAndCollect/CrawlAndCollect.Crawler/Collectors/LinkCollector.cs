using System;
using System.Collections.Generic;
using System.Linq;
using CrawlAndCollect.Crawler.Collectors.Interface;
using CrawlAndCollect.Crawler.Elements;
using CrawlAndCollect.Crawler.WarningLog;
using HtmlAgilityPack;

namespace CrawlAndCollect.Crawler.Collectors {
    public class LinkCollector : ICollector {

        public CrawlerLog Log { get; set; }
        public List<AElement> AllLinks { get; set; }
        public List<AElement> ExternalLinks {
            get {
                if(BaseUri == null || !AllLinks.Any())
                    return new List<AElement>();

                return AllLinks.Where(x => GetDomain(x.Href.Host) != GetDomain(BaseUri.Host)).ToList();
            }
        }
        public List<AElement> InternalLinks {
            get
            {
                if (BaseUri == null || !AllLinks.Any())
                    return new List<AElement>();

                return AllLinks.Where(x => GetDomain(x.Href.Host) == GetDomain(BaseUri.Host)).ToList();
            }
        }

        public bool HasLinks { get { return AllLinks != null && AllLinks.Any(); } }

        public Uri BaseUri { get; private set; }

        public LinkCollector() {
            AllLinks = new List<AElement>();
        }

        public void Collect(Uri baseUri, HtmlDocument doc) {
            BaseUri = baseUri;
            var links = doc.DocumentNode.SelectNodes("//a");

            foreach (var link in links) {

                var text = link.InnerText;
                var href = link.GetAttributeValue("href", Defaults.NoHref);
                var noFollow = link.GetAttributeValue("rel", Defaults.NoNoFollow);

                //see if link has href
                if(href == Defaults.NoHref) 
                    continue;

                href = AddHostIfRelative(href, BaseUri);

                Uri hrefUri;
                if (!Uri.TryCreate(href, UriKind.Absolute, out hrefUri))
                {
                    if(IgnoreLogging(href))
                        continue;
                    Log.Log("Href not valid Uri", "Failed to parse href to uri. Href: " + href);
                    continue;
                }

                var isNoFollow = noFollow != Defaults.NoNoFollow && noFollow.ToLower() == "nofollow";

                AllLinks.Add(new AElement(text, hrefUri, isNoFollow));
            }
        }

        /// <summary>
        /// If Href cant be parsed as uri this methos determens if its still worth logging
        /// </summary>
        /// <param name="href">The "href" wich could not be parsed</param>
        private bool IgnoreLogging(string href) {
            return string.IsNullOrWhiteSpace(href) ||
                   href.Trim() == "#";
        }

        /// <summary>
        /// Returns the domain and topdomain without www
        /// </summary>
        internal static string GetDomain(string host) {
            return host.StartsWith("www.") ? host.Substring(4) : host;
        }

        /// <summary>
        /// Applies the host domain if the link is an internal
        /// </summary>
        internal static string AddHostIfRelative(string link, Uri host)
        {
            if (link.StartsWith("/"))
                return host.Scheme + "://" + host.Host + link;
            return link;
        }

        internal static class Defaults {
            public const string NoHref = "==NOHREF==";
            public const string NoNoFollow = "==NONOFOLLOW==";
        }

    }
}