using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MimeKit;
using BcmmOja.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using BcmmOja.Services;

namespace BcmmOja.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IOptions<EmailSettings> _mailSettings;
        public ValuesController(IOptions<EmailSettings> mailSettings)
        {
            _mailSettings = mailSettings;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

            //MimeMessage message = new MimeMessage();

            //MailboxAddress from = new MailboxAddress(_mailSettings.SenderName, _mailSettings.Sender);
            //message.From.Add(from);

            //MailboxAddress to = new MailboxAddress("Ralwinn", "ralwinnjohnson@gmail.com");
            //message.To.Add(to);

            //message.Subject = "This is email subject";
            //BodyBuilder bodyBuilder = new BodyBuilder();
            //bodyBuilder.HtmlBody = "<h1>Hello World!</h1>";

            //message.Body = bodyBuilder.ToMessageBody();

            //SmtpClient client = new SmtpClient();
            //client.Connect(_mailSettings.MailServer, _mailSettings.MailPort, true);
            //client.Authenticate(_mailSettings.Sender, _mailSettings.Password);
            //client.Send(message);
            //client.Disconnect(true);
            //client.Dispose();
            var mailer = new EmailSender(_mailSettings);
            var data = mailer.SendEmail("", "", "");

            return new string[] { data.ToString() };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
