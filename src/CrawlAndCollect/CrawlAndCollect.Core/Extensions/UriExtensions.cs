using System;

namespace CrawlAndCollect.Core.Extensions {
    public static class UriExtensions {
        public static string DomainAndTopDomainWitoutWww(this Uri uri)
        {
            return uri.Host.StartsWith("www.") ? uri.Host.Substring(4) : uri.Host;
        }
    }
}