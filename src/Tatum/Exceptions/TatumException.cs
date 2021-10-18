using System;
using System.Net;
using TatumPlatform.Model;

namespace TatumPlatform
{
    public class TatumException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; private set; }
        public TatumError TatumError { get; private set; }
        public TatumException() : base()
        {

        }
        public TatumException(string message) : base(message)
        {
            this.TatumError = TatumHelper.DeserializeObject<TatumError>(message);
        }

        public TatumException(string message, HttpStatusCode httpStatusCode) : base(message)
        {
            HttpStatusCode = httpStatusCode;
            this.TatumError = TatumHelper.DeserializeObject<TatumError>(message);
        }

        public TatumException(string message, Exception innerException) : base(message, innerException)
        {
            this.TatumError = TatumHelper.DeserializeObject<TatumError>(message);
        }
    }
}
