using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Clouder.Server.Helper.Email
{
    public class SendGrid
    {
        private string key;
        public SendGrid(string key)
        {
            this.key = key;
        }
        public async Task Send(string from, string name, string subject, EmailAddress to, string content)
        {
            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress(from, name));
            var recipients = new List<EmailAddress> { to };
            msg.AddTos(recipients);
            msg.SetSubject(subject);
            msg.HtmlContent = content;
//            string apiKey = "";
//#if DEBUG
//            apiKey = Configuration.SendGridApiKey;
//#else
//            apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
//#endif
            var client = new SendGridClient(key);

            try
            {
                var response = await client.SendEmailAsync(msg);
            }
            catch (Exception e)
            {

            }
        }
    }
}
