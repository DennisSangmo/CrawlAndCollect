using System;
using System.Web.Mvc;
using CrawlAndCollect.Core.Entities.Page;
using CrawlAndCollect.Core.NserviceBus.Messages;
using CrawlAndCollect.Core.Services;
using CrawlAndCollect.Web.Controllers.Home.ViewModels;
using NServiceBus;

namespace CrawlAndCollect.Web.Controllers.Home
{
    public class HomeController : Controller
    {
        private readonly EntityService _entityService;
        private readonly IBus _bus;

        public HomeController(EntityService entityService, IBus bus) {
            _entityService = entityService;
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
            
            var page = new Page(new Uri(model.Url));

            _entityService.Session.Store(page);

            _bus.Send(new CrawlUrlMessage(page.Uri));

            return RedirectToAction("AddUrl");
        }
    }
}
