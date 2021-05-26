using System;

namespace GenericApp.Infra.Common.Exceptions
{
    [Serializable]
    public class GlobalException : Exception
    {
        public GlobalException()
        {
        }
        public GlobalException(string message) : base(message)
        {

        }

        protected GlobalException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)

        {
        }

        public GlobalException(string message, Exception innerException)
             : base(message, innerException)
        {
        }
    }
}