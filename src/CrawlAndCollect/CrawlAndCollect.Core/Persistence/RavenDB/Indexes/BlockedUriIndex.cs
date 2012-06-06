using System.Linq;
using CrawlAndCollect.Core.Entities.BlockUri;
using Raven.Client.Indexes;

namespace CrawlAndCollect.Core.Persistence.RavenDB.Indexes {
    public class BlockedUriIndex : AbstractIndexCreationTask<BlockUri>{
        public BlockedUriIndex() {
            Map = uris => from uri in uris
                          select new {uri.Uri};
        }
    }
}