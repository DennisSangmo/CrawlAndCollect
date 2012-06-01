using System.Web.Mvc;
using CrawlAndCollect.Core.Services;
using CrawlAndCollect.Web.Controllers.Log.ViewModels;

namespace CrawlAndCollect.Web.Controllers.Log {
    public class LogController : Controller {
        private readonly EntityService _entityService;

        public LogController(EntityService entityService) {
            _entityService = entityService;
        }

        public ActionResult Index() {
            var log = _entityService.GetLog();

            return View(new IndexViewModel(log));
        }
    }
}