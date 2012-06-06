using System;
using CrawlAndCollect.Core.Entities.BlockUri;
using CrawlAndCollect.Core.Persistence.RavenDB;
using CrawlAndCollect.Core.Services;
using NUnit.Framework;
using Raven.Client;

namespace CrawlAndCollect.Core.Tests.ServiceTests {
    [TestFixture]
    public class BlockTests {
        public IDocumentSession Session { get; set; }
        public EntityService EntityService { get; set; }

        [SetUp]
        public void SetUp() {
            Session = DocumentStoreFactory.CreateMemoryStore().OpenSession();
            EntityService = new EntityService(Session);
        }

        [Test]
        public void Should_block_uri() {
            var uri = "http://www.dennissangmo.se/";
            
            var blockUri = new BlockUri(new Uri(uri));
            Session.Store(blockUri);
            Session.SaveChanges();

            var toValidate = new Uri(uri);

            var blocked = EntityService.IsPageBlocked(toValidate);

            Assert.IsTrue(blocked);
        }

        [Test]
        public void Should_block_uri_by_domain()
        {
            var uri = "http://www.dennissangmo.se/subcategory";

            var blockDomain = new BlockDomain("dennissangmo.se");
            Session.Store(blockDomain);
            Session.SaveChanges();

            var toValidate = new Uri(uri);

            var blocked = EntityService.IsPageBlocked(toValidate);

            Assert.IsTrue(blocked);
        }

        [Test]
        public void Should_block_uri_by_domain_without_www()
        {
            var uri = "http://dennissangmo.se/subcategory";

            var blockDomain = new BlockDomain("dennissangmo.se");
            Session.Store(blockDomain);
            Session.SaveChanges();

            var toValidate = new Uri(uri);

            var blocked = EntityService.IsPageBlocked(toValidate);

            Assert.IsTrue(blocked);
        }
    }
}