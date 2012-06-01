using CrawlAndCollect.Core.IoC;
using NServiceBus;

namespace CrawlAndCollect.LinkValidatorEndPoint
{
    public class EndPointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
    {
        public void Init()
        {
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
