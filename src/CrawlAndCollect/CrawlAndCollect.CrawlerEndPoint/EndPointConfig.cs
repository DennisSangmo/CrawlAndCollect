using CrawlAndCollect.Core.IoC;
using NServiceBus;

namespace CrawlAndCollect.CrawlerEndPoint {
    public class EndPointConfig : IConfigureThisEndpoint, IWantCustomInitialization, AsA_Publisher{
        public void Init() {
            Configure.With()
                     .DefaultBuilder()
                     .StructureMapBuilder(CrawlAndCollectContainer.GetEndPointContainer())
                     .XmlSerializer()
                     .Log4Net()
                     .MsmqTransport()
                     .UnicastBus()
                     .CreateBus()
                     .Start(() => Configure.Instance.ForInstallationOn<NServiceBus.Installation.Environments.Windows>().Install());
        }
    }
}