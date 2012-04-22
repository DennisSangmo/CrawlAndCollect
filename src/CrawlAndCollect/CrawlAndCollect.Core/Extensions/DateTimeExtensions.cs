using System;

namespace CrawlAndCollect.Core.Extensions {
    public static class DateTimeExtensions {
        public static string ToDateTimeString(this DateTime instance) {
            return instance.ToShortDateString() + " " + instance.ToShortTimeString();
        }
    }
}