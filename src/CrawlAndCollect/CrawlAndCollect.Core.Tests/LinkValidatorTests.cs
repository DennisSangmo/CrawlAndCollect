using System;
using CrawlAndCollect.Core.Bs;
using CrawlAndCollect.Core.Entities.BlockUri;
using CrawlAndCollect.Core.Persistence.RavenDB;
using NUnit.Framework;
using Raven.Client;
using System.Linq;

namespace CrawlAndCollect.Core.Tests {
    [TestFixture]
    public class LinkValidatorTests {
        private IDocumentSession Session { get; set; }
        private LinkValidator Validator { get; set; }
        
        [SetUp]
        public void SetUp() {
            Session = DocumentStoreFactory.CreateMemoryStore().OpenSession();
            Validator = new LinkValidator(Session);
        }

        [Test]
        public void OnlySwedishTopDomain(){
            var se = Validator.Validate(new Uri("http://dennissangmo.se"));
            var nu = Validator.Validate(new Uri("http://dennissangmo.nu"));
            var com = Validator.Validate(new Uri("http://dennissangmo.com"));

            Assert.IsTrue(se);
            Assert.IsTrue(nu);
            Assert.IsFalse(com);
        }

        [Test]
        public void ShouldBlockUri() {
            var uri = new Uri("http://dennissangmo.se");

            Session.Store(new BlockUri(uri));
            Session.SaveChanges();
            var s = Session.Query<BlockUri>().ToList();
            var res = Validator.Validate(uri);

            Assert.IsFalse(res);
        }
    }
}