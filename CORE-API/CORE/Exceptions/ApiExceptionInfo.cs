using System;

namespace CORE_API.CORE.Exceptions
{
    public class ApiExceptionInfo
    {
        public ApiExceptionInfo(Exception e)
        {
            ExceptionType = e.GetType().ToString();
            Message = e.Message;
            InnerException = e.InnerException?.ToString();
            InnerExceptionStackTrace = e.InnerException?.StackTrace?.ToString();
            StackTrace = e.StackTrace?.ToString();
        }

        public string ExceptionType { get; set; }

        public string Message { get; set; }

        public string InnerException { get; set; }
        
        public string InnerExceptionStackTrace { get; set; }

        public string StackTrace { get; set; }
    }
}

