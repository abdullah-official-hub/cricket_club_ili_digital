using System;
using System.Net;

namespace Framework.Exceptions
{
    public class StatusCodeException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Type { get; set; }

        public StatusCodeException(string message, string type, HttpStatusCode httpStatusCode) : base(message)
        {
            HttpStatusCode = httpStatusCode;
            Type = type;
        }
    }
}