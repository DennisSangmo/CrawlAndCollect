using System;
using CrawlAndCollect.Core.Services;
using NServiceBus.UnitOfWork;

//Not needed anymore atm

namespace CrawlAndCollect.Core.NserviceBus.UnitOfWork {
    public class MyRavenUnitOfWork : IManageUnitsOfWork{

        public void Begin() {
        }

        public void End(Exception ex = null) {
            if(ex == null) {
            }
        }
    }
}