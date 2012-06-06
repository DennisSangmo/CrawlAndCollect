using System.Linq;
using CrawlAndCollect.Core.Entities.BlockUri;
using Raven.Client.Indexes;

namespace CrawlAndCollect.Core.Persistence.RavenDB.Indexes {
    public class BlockedDomainIndex : AbstractIndexCreationTask<BlockDomain>{
        public BlockedDomainIndex() {
            Map = uris => from uri in uris
                          select new {uri.Domain};
        }
    }
}