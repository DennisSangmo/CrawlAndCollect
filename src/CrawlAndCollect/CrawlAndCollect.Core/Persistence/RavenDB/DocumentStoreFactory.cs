using System;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Raven.Client.Indexes;

namespace CrawlAndCollect.Core.Persistence.RavenDB {
    public static class DocumentStoreFactory {

        private static string _entityConnectionString = "RavenDBEnties";
        public static IDocumentStore CreateMemoryStore()
        {
            var ds = new EmbeddableDocumentStore { RunInMemory = true, };
            ds.Initialize();
            //ApplyIndexes(ds);
            return ds;
        }

        public static IDocumentStore CreateLiveStore()
        {
            var ds = new DocumentStore { ConnectionStringName = _entityConnectionString };
            ds.Initialize();
            ApplyIndexes(ds);
            return ds;
        }

        public static IDocumentStore CreateEndPointStore(Guid resourceManagerId) {
            var ds = new DocumentStore {
                                           ConnectionStringName = _entityConnectionString, 
                                           ResourceManagerId = resourceManagerId
                                       };
            ds.Initialize();
            ApplyIndexes(ds);
            return ds;
        }

        private static void ApplyIndexes(IDocumentStore documentStore)
        {
            IndexCreation.CreateIndexes(typeof(DocumentStoreFactory).Assembly, documentStore);
        }
    }
}