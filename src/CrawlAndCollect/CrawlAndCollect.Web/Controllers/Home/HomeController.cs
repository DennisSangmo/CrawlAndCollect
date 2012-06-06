using System;
using System.Web.Mvc;
using CrawlAndCollect.Core.Entities.CrawledUri;
using CrawlAndCollect.Core.Entities.Link;
using CrawlAndCollect.Core.NserviceBus.Messages;
using CrawlAndCollect.Web.Controllers.Home.ViewModels;
using NServiceBus;
using Raven.Client;
using System.Linq;

namespace CrawlAndCollect.Web.Controllers.Home
{
    public class HomeController : BaseController
    {
        private readonly IBus _bus;
        private readonly IDocumentSession _session;

        public HomeController(IBus bus, IDocumentSession session) {
            _bus = bus;
            _session = session;
        }

        public ActionResult Index() {
            var lastCrawled = _session.Query<CrawledUri>().OrderByDescending(x => x.Crawled).Take(1).FirstOrDefault();
            var pages = _session.Query<CrawledUri>().Count();
            var links = _session.Query<Link>().Count();
            return View(new IndexViewModel(lastCrawled, pages, links));
        }

        [HttpGet]
        public ActionResult AddUrl() {
            return View(new AddUrlViewModel());
        }

        [HttpPost]
        public ActionResult AddUrl(AddUrlViewModel model) {
            if (!ModelState.IsValid)
                return AddUrl();

            // TODO Validate url

            var uri = new Uri(model.Url);

            _bus.Send(new RequestUriCrawlMessage(uri));

            return RedirectToAction("AddUrl");
        }
    }
}
