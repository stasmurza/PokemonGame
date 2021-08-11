using System;

namespace ApiGateway.BusinessLayer.Exceptions
{
    public class ApiBadRequestException : Exception
    {
        public ApiBadRequestException(string message) : base(message) { }
    }
}
