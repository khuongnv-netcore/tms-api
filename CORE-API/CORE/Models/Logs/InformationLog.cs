using System;
using System.Text.Json;

namespace CORE_API.CORE.Models.Logs
{
    public class InformationLog
    {
        public DateTime? Date { get; set; }

        public string Url { get; set; }

        public string QueryString { get; set; }

        public JsonDocument RequestBody { get; set; }

        public string Message { get; set; }

        public string UserAgent { get; set; }

        public string UserName { get; set; }

        public string Tag { get; set; }

        public string LogLevel { get; set; }
    }
}
