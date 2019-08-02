using BcmmOja.Utility;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BcmmOja.Services
{
    
    public class EmailSender
    {
        public readonly EmailSettings _mailSettings;
        private EmailSettings mailSettings;

        public EmailSender(IOptions<EmailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public EmailSender()
        {
        }

        public EmailSender(EmailSettings mailSettings)
        {
            this.mailSettings = mailSettings;
        }

        public async Task<bool> SendEmail(string ToName, string ToEmail, string ToSubject, string ToBody )
        {
            try
            {
                MimeMessage message = new MimeMessage();

                MailboxAddress from = new MailboxAddress(_mailSettings.SenderName, _mailSettings.Sender);
                message.From.Add(from);

                MailboxAddress to = new MailboxAddress(ToName, ToEmail);
                message.To.Add(to);

                message.Subject = ToSubject;
                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = ToBody;

                message.Body = bodyBuilder.ToMessageBody();

                SmtpClient client = new SmtpClient();
                await client.ConnectAsync(_mailSettings.MailServer, _mailSettings.MailPort, true);
                await client.AuthenticateAsync(_mailSettings.Sender, _mailSettings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                client.Dispose();
                return true;
            }
            catch (System.Exception ex)
            {
                // throw ex;
                return false;
            }
            
        }
    }
}
