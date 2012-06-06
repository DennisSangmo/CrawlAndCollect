using System.Linq;
using CrawlAndCollect.Core.Entities.CrawledUri;
using Raven.Client.Indexes;

namespace CrawlAndCollect.Core.Persistence.RavenDB.Indexes {
    public class CrawledUriIndex : AbstractIndexCreationTask<CrawledUri>{
        public CrawledUriIndex() {
            Map = uris => from uri in uris
                          select new {uri.Uri};
        }
    }
}