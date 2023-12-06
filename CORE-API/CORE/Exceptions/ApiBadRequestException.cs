using System;
using System.Collections.Generic;

namespace CORE_API.CORE.Exceptions
{
    public class ApiBadRequestException : Exception
    {
        public ApiBadRequestException() : base()
        {

        }

        public ApiBadRequestException(string message) : base(message)
        {

        }

        public ApiBadRequestException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
