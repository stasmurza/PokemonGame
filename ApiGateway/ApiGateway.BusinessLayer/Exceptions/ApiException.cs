using System;

namespace ApiGateway.BusinessLayer.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string message) : base(message) { }
    }
}
