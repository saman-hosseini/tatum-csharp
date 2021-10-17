using System;
using System.Net;

namespace TatumPlatform
{
    public class TatumException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; private set; }
        public TatumException() : base()
        {

        }
        public TatumException(string message) : base(message)
        {

        }

        public TatumException(string message, HttpStatusCode httpStatusCode) : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }

        public TatumException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
