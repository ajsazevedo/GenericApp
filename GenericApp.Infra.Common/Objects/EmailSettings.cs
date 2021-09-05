using Microsoft.Extensions.Configuration;

namespace GenericApp.Infra.Common.Objects
{
    public class EmailSettings
    {
        public string Sender { get; set; }
        public string SenderName { get; set; }
        public string SenderPassword { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Server { get; set; }

        public EmailSettings()
        {
        }

        public EmailSettings(IConfigurationSection section)
        {
            var mailSettings = section.Get<EmailSettings>();
            Sender = mailSettings.Sender;
            SenderName = mailSettings.SenderName;
            SenderPassword = mailSettings.SenderPassword;
            Host = mailSettings.Host;
            Port = mailSettings.Port;
        }
    }
}
