using System;
using System.Web.Mvc;
using CrawlAndCollect.Core.NserviceBus.Messages;
using CrawlAndCollect.Web.Controllers.Home.ViewModels;
using NServiceBus;

namespace CrawlAndCollect.Web.Controllers.Home
{
    public class HomeController : BaseController
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

            var uri = new Uri(model.Url);

            _bus.Send(new CrawlUrlMessage(uri));

            return RedirectToAction("AddUrl");
        }
    }
}
