using System.Collections.Generic;
using System.Linq;
using CrawlAndCollect.Core.Entities.Log;

namespace CrawlAndCollect.Web.Controllers.Log.ViewModels {
    public class IndexViewModel {
        public IEnumerable<LogRow> Log { get; set; }
        public bool HasLogs { get { return Log != null && Log.Any(); } }

        public IndexViewModel(IEnumerable<LogRow> log) {
            Log = log;
        }
    }
}