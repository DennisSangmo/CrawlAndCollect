using System;
using System.Collections.Generic;
using System.Linq;
using CrawlAndCollect.Crawler.Collectors.Interface;
using CrawlAndCollect.Crawler.WarningLog;
using HtmlAgilityPack;

namespace CrawlAndCollect.Crawler.Collectors {
    /// <summary>
    /// An example of an collector wich will collect all images of a page
    /// </summary>
    public class ImageCollector : ICollector {
        public List<Uri> ImageUris { get; set; }
        public CrawlerLog Log { get; set; }

        public ImageCollector(){
            ImageUris = new List<Uri>();
        }

        public void Collect(Uri baseUri, HtmlDocument doc) {
            var images = doc.DocumentNode.SelectNodes("//img");

            foreach (var image in images) {
                var unsafeSrc = image.Attributes.SingleOrDefault(x => x.Name == "src");
                if (unsafeSrc == null) 
                    continue;

                var src = unsafeSrc.Value;

                var srcUri = src.StartsWith("/") ? new Uri(baseUri.AbsoluteUri + src) : new Uri(src);

                if(ImageUris.Any(x => x.AbsoluteUri == srcUri.AbsoluteUri))
                    continue;
                ImageUris.Add(srcUri);
            }
        }

    }
}