using System;
using CrawlAndCollect.Core.Entities.Base;

namespace CrawlAndCollect.Core.Entities.BlockUri {
    public class BlockUri : Entity {
        public Uri Uri { get; set; }
        public string Comment { get; set; }
        public DateTime Blocked { get; set; }

        public BlockUri(Uri uri, string comment = "") {
            Uri = uri;
            Comment = comment;
            Blocked = DateTime.Now;
        }
    }
}