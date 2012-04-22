using System.ComponentModel;

namespace CrawlAndCollect.Core.Entities.Log {
    public enum LogLevel {
        [Description("Critical error")]
        CriticalError = 1,
        [Description("Error")]
        Error,
        [Description("Warning")]
        Warning,
        [Description("Information")]
        Information
    }
}