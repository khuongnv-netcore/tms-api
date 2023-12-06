using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_API.CORE.Exceptions
{
    public class ApiNotAuthorizedException : Exception
    {
        public ApiNotAuthorizedException() : base()
        {

        }

        public ApiNotAuthorizedException(string message) : base(message)
        {

        }

        public ApiNotAuthorizedException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
