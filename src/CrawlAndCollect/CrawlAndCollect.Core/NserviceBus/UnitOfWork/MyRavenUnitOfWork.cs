using System;
using NServiceBus.UnitOfWork;
using Raven.Client;

namespace CrawlAndCollect.Core.NserviceBus.UnitOfWork {
    public class MyRavenUnitOfWork : IManageUnitsOfWork{
        private readonly IDocumentSession _session;

        public MyRavenUnitOfWork(IDocumentSession session) {
            _session = session;
        }

        public void Begin() {
        }

        public void End(Exception ex = null) {
            if(ex == null) {
                _session.SaveChanges();
            }
        }
    }
}