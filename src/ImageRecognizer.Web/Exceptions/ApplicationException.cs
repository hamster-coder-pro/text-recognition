using System;
using System.Runtime.Serialization;

namespace ImageRecognizer.Web.Exceptions
{
    [Serializable]
    public class ApplicationException : Exception
    {
        public ApplicationException()
        {
        }

        protected ApplicationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public ApplicationException(string message)
            : base(message)
        {
        }

        public ApplicationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}