using System;

namespace CrawlAndCollect.Core.Entities.Log {
    public class LogRow {

        public DateTime Raised { get; set; }
        public LogLevel LogLevel { get; set; }
        public LogSender Sender { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public LogRow(DateTime raised, LogLevel logLevel, LogSender sender, string title, string description) {
            Raised = raised;
            LogLevel = logLevel;
            Sender = sender;
            Title = title;
            Description = description;
        }
    }
}