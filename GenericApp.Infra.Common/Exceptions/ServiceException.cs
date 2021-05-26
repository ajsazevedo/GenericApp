using System;

namespace GenericApp.Infra.Common.Exceptions
{
    [Serializable]
    public class ServiceException : GlobalException
    {
        public string FriendlyMessage { get; set; }
        public ServiceException()
        {
        }

        public ServiceException(string message, string friendlyMessage = null) : base(message)
        {
            FriendlyMessage = friendlyMessage;
        }

        protected ServiceException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)

        {
        }

        public ServiceException(string message, Exception innerException)
             : base(message, innerException)
        {
        }
    }
}