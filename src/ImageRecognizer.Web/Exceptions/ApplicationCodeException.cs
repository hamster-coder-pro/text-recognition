using System;
using System.Runtime.Serialization;

namespace ImageRecognizer.Web.Exceptions
{
    [Serializable]
    public class ApplicationCodeException : ApplicationException
    {
        public string Code { get; set; }

        public ApplicationCodeException()
        {
        }

        protected ApplicationCodeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public ApplicationCodeException(string code, string message)
            : base(message)
        {
            Code = code;
        }

        public ApplicationCodeException(string code, string message, Exception innerException)
            : base(message, innerException)
        {
            Code = code;
        }
    }
}