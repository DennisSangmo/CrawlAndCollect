using System;
using System.Linq;
using CrawlAndCollect.Crawler.Collectors;
using CrawlAndCollect.Crawler.Elements;
using CrawlAndCollect.Crawler.WarningLog;
using HtmlAgilityPack;
using NUnit.Framework;

namespace CrawlAndCollect.Crawler.Tests.Collectors {
    [TestFixture]
    public class LinkCollectorTests {
        private HtmlDocument ToHtmlDocument(string html) {

            var doc = new HtmlDocument
            {
                OptionAddDebuggingAttributes = false,
                OptionAutoCloseOnEnd = true,
                OptionFixNestedTags = true,
                OptionReadEncoding = true
            };
            doc.LoadHtml(html);
            return doc;
        }

        private LinkCollector CreateLinkCollector(string uri, string htmlString) {
            var baseUri = new Uri(uri);
            var html = ToHtmlDocument(htmlString);
            var linkCollector = new LinkCollector{Log = new CrawlerLog()};
            linkCollector.Collect(baseUri, html);
            return linkCollector;
        }

        private void TestAElement(string id, AElement aElement, string expectedText, string expectedHref, bool expectedNoFollow = false)
        {
            Assert.AreEqual(expectedText, aElement.Text, "[" + id + "] TextFail");
            Assert.AreEqual(expectedHref, aElement.Href.OriginalString, "[" + id + "] HrefFail");
            Assert.AreEqual(expectedNoFollow, aElement.NoFollow, "[" + id + "] NoFollowFail");
        }

         [Test]
        public void Should_return_all_links() {
             // ASSIGN
             var html = "<html><head></head><body><a href=\"http://dennissangmo.se/linkone.html\">Link One</a><a href=\"http://dennissangmo.se/linktwo.html\">Link Two</a></body></html>";
             var linkCollector = CreateLinkCollector("http://dennissangmo.se", html);

             // ACT
             var links = linkCollector.AllLinks;

             // ASSERT
             Assert.AreEqual(2, links.Count, "Do not contain two links");
             TestAElement("0", links[0], "Link One", "http://dennissangmo.se/linkone.html");
             TestAElement("1", links[1], "Link Two", "http://dennissangmo.se/linktwo.html");
         }

         [Test]
         public void Should_make_relative_to_full_href() {
             // ASSIGN
             var html = "<html><head></head><body><a href=\"/relative.html\">relative</a></body></html>";
             var linkCollector = CreateLinkCollector("http://dennissangmo.se", html);

             // ACT
             var links = linkCollector.AllLinks;

             // ASSERT
             Assert.AreEqual(1, links.Count, "Do not contain 1 links");
             TestAElement("0", links[0], "relative", "http://dennissangmo.se/relative.html");
         }

         [Test]
         public void Should_mark_as_nofollow() {
             // ASSIGN
             var html = "<html><head></head><body><a href=\"http://dennissangmo.se/nofollow.html\" rel=\"nofollow\">nofollow</a></body></html>";
             var linkCollector = CreateLinkCollector("http://dennissangmo.se", html);

             // ACT
             var links = linkCollector.AllLinks;

             // ASSERT
             Assert.AreEqual(1, links.Count, "Do not contain 1 links");
             TestAElement("0", links[0], "nofollow", "http://dennissangmo.se/nofollow.html", true);
         }

         [Test]
         public void Should_get_all_external()
         {
             // ASSIGN
             var html = "<html><head></head><body><a href=\"http://dennissangmo.se/internal.html\">internal</a><a href=\"http://other.se/external.html\">external</a></body></html>";
             var linkCollector = CreateLinkCollector("http://dennissangmo.se", html);

             // ACT
             var links = linkCollector.ExternalLinks;

             // ASSERT
             Assert.AreEqual(1, links.Count, "Do not contain 1 links");
             TestAElement("0", links[0], "external", "http://other.se/external.html");
         }

         [Test]
         public void Should_get_all_internal()
         {
             // ASSIGN
             var html = "<html><head></head><body><a href=\"http://dennissangmo.se/internal.html\">internal</a><a href=\"http://other.se/external.html\">external</a></body></html>";
             var linkCollector = CreateLinkCollector("http://dennissangmo.se", html);

             // ACT
             var links = linkCollector.InternalLinks;

             // ASSERT
             Assert.AreEqual(1, links.Count, "Do not contain 1 links");
             TestAElement("0", links[0], "internal", "http://dennissangmo.se/internal.html");
         }

         [Test]
         public void Should_log_if_cant_parse_href()
         {
             // ASSIGN
             var html = "<html><head></head><body><a href=\"ImNotAUriArgh\">badlink</a></body></html>";
             var linkCollector = CreateLinkCollector("http://dennissangmo.se", html);

             // ACT
             var links = linkCollector.AllLinks;
             var log = linkCollector.Log.GetLog().ToList();

             // ASSERT
             Assert.AreEqual(0, links.Count, "Shouldnt have found any links");
             Assert.AreEqual(1, log.Count(), "Should have raised a logwarning");
             Assert.AreEqual("Href not valid Uri", log[0].Title);
         }

    }
}
