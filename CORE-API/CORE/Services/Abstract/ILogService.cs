using System;

namespace CORE_API.CORE.Services.Abstract
{
    public interface ILogService
    {
        void LogException(Exception exception, string tag = null);

        void LogInformation(string information, string tag = null);
        void LogInternalMessage(string message);

    }
}
