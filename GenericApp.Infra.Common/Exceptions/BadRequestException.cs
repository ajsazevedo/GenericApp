using System;

namespace GenericApp.Infra.Common.Exceptions
{
    [Serializable]
    public class BadRequestException : GlobalException
    {
        public string FriendlyMessage { get; set; }
        public BadRequestException()
        {
        }

        public BadRequestException(string message) : base(message)
        {
            FriendlyMessage = message;
        }

        public BadRequestException(string message, string friendlyMessage = null) : base(message)
        {
            FriendlyMessage = friendlyMessage;
        }

        protected BadRequestException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)

        {
        }

        public BadRequestException(string message, Exception innerException)
             : base(message, innerException)
        {
        }
    }
}