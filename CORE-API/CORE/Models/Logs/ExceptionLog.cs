using System;
using System.Text.Json;

namespace CORE_API.CORE.Models.Logs
{
    public class ExceptionLog
    {
        public string ExceptionType { get; set; }

        public DateTime? ExceptionDate { get; set; }

        public string Url { get; set; }

        public string QueryString { get; set; }

        public JsonDocument RequestBody { get; set; }

        public string Message { get; set; }

        public string InnerException { get; set; }

        public string StackTrace { get; set; }

        public string UserAgent { get; set; }

        public string UserName { get; set; }

        public string Tag { get; set; }

        public string LogLevel { get; set; }
    }
}
