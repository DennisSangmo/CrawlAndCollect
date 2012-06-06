using System;
using CrawlAndCollect.Core.Entities.Base;

namespace CrawlAndCollect.Core.Entities.BlockUri {
    public class BlockDomain : Entity {
        /// <summary>
        /// Domain in the simplest form with topdomain. Ex: "domain.se"
        /// </summary>
        public string Domain { get; set; }
        public string Comment { get; set; }
        public DateTime Blocked { get; set; }

        public BlockDomain(string domain, string comment = "") {
            Domain = domain;
            Comment = comment;
            Blocked = DateTime.Now;
        }
    }
}