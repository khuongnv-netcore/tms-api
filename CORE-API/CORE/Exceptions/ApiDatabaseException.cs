using System;

namespace CORE_API.CORE.Exceptions
{
    public class ApiDatabaseException : Exception
    {
        public ApiDatabaseException() : base()
        {

        }

        public ApiDatabaseException(string message) : base(message)
        {

        }

        public ApiDatabaseException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
