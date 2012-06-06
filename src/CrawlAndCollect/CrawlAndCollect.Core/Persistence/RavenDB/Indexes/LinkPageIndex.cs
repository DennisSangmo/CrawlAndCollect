using System.Linq;
using CrawlAndCollect.Core.Entities.Link;
using Raven.Client.Indexes;

namespace CrawlAndCollect.Core.Persistence.RavenDB.Indexes {
    public class LinkPageIndex : AbstractIndexCreationTask<Link>
    {
        public LinkPageIndex()
        {
            Map = links => from link in links
                           select new {link.Page};
        }
    }
}