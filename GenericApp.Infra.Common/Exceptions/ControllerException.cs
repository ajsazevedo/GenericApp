using System;

namespace GenericApp.Infra.Common.Exceptions
{
    [Serializable]
    public class ControllerException : GlobalException
    {
        public string FriendlyMessage { get; set; }
        public ControllerException()
        {
        }

        public ControllerException(string message) : base(message)
        {
            FriendlyMessage = message;
        }

        public ControllerException(string message, string friendlyMessage = null) : base(message)
        {
            FriendlyMessage = friendlyMessage;
        }

        protected ControllerException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)

        {
        }

        public ControllerException(string message, Exception innerException)
             : base(message, innerException)
        {
        }
    }
}
