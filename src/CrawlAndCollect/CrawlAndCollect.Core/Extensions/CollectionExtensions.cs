using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CrawlAndCollect.Core.Extensions {
    [DebuggerStepThrough]
    public static class CollectionExtensions {
        public static void Each<T>(this IEnumerable<T> collection, Action<T> action) {
            foreach (var item in collection) {
                action(item);
            }
        }
    }
}