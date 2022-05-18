using System.Net;

namespace Framework.Exceptions
{
    public class NotFoundException : StatusCodeException
    {
        public NotFoundException(string message) : base(message, "NotFound", HttpStatusCode.NotFound)
        {
        }
    }
}