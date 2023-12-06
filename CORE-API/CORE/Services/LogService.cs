using CORE_API.CORE.Models.Logs;
using CORE_API.CORE.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CORE_API.CORE.Services
{
    public class LogService : ILogService
    {
        private readonly ILogger<LogService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogService(ILogger<LogService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public void LogInternalMessage(string message)
        {
            _logger.LogInformation(message);
        }

        public async void LogException(Exception exception, string tag = null)
        {
            var jsonLog = await BuildExceptionLogJson(exception, tag);
            _logger.LogError(jsonLog);
        }

        public async void LogInformation(string information, string tag = null)
        {
            var jsonLog = await BuildInformationLogJson(information, tag);
            _logger.LogInformation(jsonLog);
        }

        private async Task<string> BuildExceptionLogJson(Exception exception, string tag = null)
        {
            ExceptionLog logEntry = new ExceptionLog();

            var body = await GetBodyFromRequest(_httpContextAccessor?.HttpContext);

            logEntry.ExceptionType = exception.GetType().ToString();
            logEntry.ExceptionDate = DateTime.UtcNow;
            logEntry.Url = _httpContextAccessor?.HttpContext?.Request?.Path.ToString();
            logEntry.QueryString = _httpContextAccessor?.HttpContext?.Request?.QueryString.ToString();
            logEntry.RequestBody = body;
            logEntry.Message = exception.Message;
            logEntry.InnerException = exception.InnerException?.ToString();
            logEntry.StackTrace = exception.StackTrace?.ToString();
            // Update when auth completed
            // logEntry.UserName = userName;
            // logEntry.UserAgent = userAgent;
            logEntry.Tag = tag;
            logEntry.LogLevel = LogLevel.Error.ToString();

            var json = JsonSerializer.Serialize(logEntry);

            return json;
        }

        private async Task<string> BuildInformationLogJson(string information, string tag = null)
        {
            InformationLog logEntry = new InformationLog();

            var body = await GetBodyFromRequest(_httpContextAccessor?.HttpContext);

            logEntry.Date = DateTime.UtcNow;
            logEntry.Url = _httpContextAccessor?.HttpContext?.Request?.Path.ToString();
            logEntry.QueryString = _httpContextAccessor?.HttpContext?.Request?.QueryString.ToString();
            logEntry.RequestBody = body;
            logEntry.Message = information;
            // Update when auth completed
            // logEntry.UserName = userName;
            // logEntry.UserAgent = userAgent;
            logEntry.Tag = tag;
            logEntry.LogLevel = LogLevel.Information.ToString();

            var json = JsonSerializer.Serialize(logEntry);

            return json;
        }

        private async Task<JsonDocument> GetBodyFromRequest(HttpContext httpContext)
        {
            try
            {
                httpContext.Request.EnableBuffering();
            }
            catch
            {
                throw new Exception("Unable to access request from http context.");
            }

            StreamReader reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, true, 1024, true);

            httpContext.Request.Body.Position = 0;
            

            var body = await reader.ReadToEndAsync();
            JsonDocument jsonBody = null;
            if (!string.IsNullOrEmpty(body))
            {
                try
                {
                    jsonBody = JsonDocument.Parse(body);
                }
                catch
                {
                    throw new Exception("Unable to parse request body.");
                }
            }

            httpContext.Request.Body.Position = 0;

            return jsonBody;
        }
    }
}
