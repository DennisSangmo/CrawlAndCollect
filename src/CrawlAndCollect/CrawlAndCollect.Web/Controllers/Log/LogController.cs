using System.Web.Mvc;
using CrawlAndCollect.Core.Services;
using CrawlAndCollect.Web.Controllers.Log.ViewModels;

namespace CrawlAndCollect.Web.Controllers.Log {
    public class LogController : Controller {
        public LogService LogService { get; set; }

        public LogController(LogService logService) {
            LogService = logService;
        }

        public ActionResult Index() {
            var log = LogService.GetAll();

            return View(new IndexViewModel(log));
        }
    }
}