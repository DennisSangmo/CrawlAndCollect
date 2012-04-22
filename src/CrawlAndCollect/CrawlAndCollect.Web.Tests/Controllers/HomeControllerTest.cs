using CrawlAndCollect.Core.Persistence.RavenDB;
using NUnit.Framework;
using Raven.Client;

namespace CrawlAndCollect.Web.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        public IDocumentSession Session { get; set; }

        [SetUp]
        public void SetUpper() {
            Session = SessionFactory.CreateInMemorySession();
        }
        [TearDown]
        public void TearDown() {
            Session.Advanced.Clear();
        }

        [Test]
        public void Index()
        {
        }
    }
}
