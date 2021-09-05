using System;
using System.Net.Mail;

namespace GenericApp.Infra.Common.Exceptions
{
    [Serializable]
    public class MailException : SmtpException
    {
        public string FriendlyMessage { get; set; }
        public MailException()
        {
        }

        public MailException(string message) : base(message)
        {
            FriendlyMessage = message;
        }

        public MailException(string message, string friendlyMessage) : base(message)
        {
            FriendlyMessage = friendlyMessage;
        }

        protected MailException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)

        {
        }

        public MailException(string message, Exception innerException)
             : base(message, innerException)
        {
            FriendlyMessage = message;
        }
    }
}
