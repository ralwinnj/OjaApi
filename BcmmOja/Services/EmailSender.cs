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

    
        public EmailSender(IOptions<EmailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public EmailSender(EmailSettings mailSettings)
        {
        }

        public bool SendEmail(string Message, string toName, string toEMailAddress )
        {
            try
            {
                MimeMessage message = new MimeMessage();

                MailboxAddress from = new MailboxAddress(_mailSettings.SenderName, _mailSettings.Sender);
                message.From.Add(from);

                MailboxAddress to = new MailboxAddress("Ralwinn", "ralwinnjohnson@gmail.com");
                message.To.Add(to);

                message.Subject = "This is email subject";
                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = "<h1>Hello World!</h1>";

                message.Body = bodyBuilder.ToMessageBody();

                SmtpClient client = new SmtpClient();
                client.Connect(_mailSettings.MailServer, _mailSettings.MailPort, true);
                client.Authenticate(_mailSettings.Sender, _mailSettings.Password);
                client.Send(message);
                client.Disconnect(true);
                client.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
            
        }
    }
}
