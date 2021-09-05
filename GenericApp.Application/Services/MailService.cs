using GenericApp.Application.Services.Base;
using GenericApp.Domain.Interfaces.Services;
using GenericApp.Infra.CC.Interfaces;
using GenericApp.Infra.CC.Localization.Resources;
using GenericApp.Infra.Common.Exceptions;
using GenericApp.Infra.Common.Objects;
using System.Net;
using System.Net.Mail;

namespace GenericApp.Application.Services
{
    public class MailService : BaseService, IMailService
    {
        private readonly SmtpClient smtpClient;
        private readonly MailAddress sender;
        public MailService(IApplicationManager applicationManager, EmailSettings mailSettings) : base(applicationManager)
        {
            smtpClient = new SmtpClient(mailSettings.Host, mailSettings.Port)
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(mailSettings.Sender, mailSettings.SenderPassword)
            };
            sender = new MailAddress(mailSettings.Sender, mailSettings.SenderName);
        }

        public void SendNewPassword(string newPassword, string destination, string name)
        {
            SendEmail(new MailAddress(destination, name), SharedResource.NewPasswordSubject, string.Format(SharedResource.NewPasswordMail, newPassword));
        }

        protected void SendEmail(MailAddress destination, string subject, string body)
        {
            var message = new MailMessage(sender, destination)
            {
                Body = body,
                IsBodyHtml = true,
                Subject = subject
            };
            try
            {
                smtpClient.Send(message);
            }
            catch (SmtpException ex)
            {
                throw new MailException(SharedResource.CouldNotDeliverTheEmail, ex);
            }
        }
    }
}
