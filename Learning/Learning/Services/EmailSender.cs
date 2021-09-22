using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using MimeKit;
using Microsoft.Extensions.Options;
using MailKit.Security;
using lms.Services;

namespace Services
{
    public class MailSettings
    {        
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
    public class MailContent
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
    public class EmailSender : IEmailService
    {
        private readonly MailSettings mailSettings;
        public EmailSender(MailSettings _mailSettings)
        {
            mailSettings = _mailSettings;
        }
        public async Task SendMail(MailContent mailContent)
        {
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail);
            email.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail));
            email.To.Add(MailboxAddress.Parse(mailContent.To));
            email.Subject = mailContent.Subject;


            var builder = new BodyBuilder();
            builder.HtmlBody = mailContent.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(mailSettings.Mail, mailSettings.Password);
                await smtp.SendAsync(email);
            }
            catch
            {
                System.IO.Directory.CreateDirectory("mailssave");
                var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                await email.WriteToAsync(emailsavefile);
            }

            smtp.Disconnect(true);

        }
        public async Task SendConfirmEmailAsync (string email, string link, string linkMessage, string subject)
        {
            await SendMail(new MailContent()
            {
                To = email,
                Subject = subject,
                Body = "<a href='" + link + "'>" + linkMessage +  "</a>"
            });
        }
    }
}
