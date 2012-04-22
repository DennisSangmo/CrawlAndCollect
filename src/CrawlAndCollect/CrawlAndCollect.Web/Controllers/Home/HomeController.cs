using System;
using System.Web.Mvc;
using CrawlAndCollect.Core.Entities.PageUrl;
using CrawlAndCollect.Core.NserviceBus.Messages;
using CrawlAndCollect.Web.Controllers.Home.ViewModels;
using NServiceBus;

namespace CrawlAndCollect.Web.Controllers.Home
{
    public class HomeController : Controller
    {
        private readonly IBus _bus;

        public HomeController(IBus bus) {
            _bus = bus;
        }

        public ActionResult Index() {
            return View();
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
            
            var url = new PageUrl(new Uri(model.Url));

            _bus.Send(new CrawlUrlMessage(url));

            return RedirectToAction("AddUrl");
        }
    }
}
