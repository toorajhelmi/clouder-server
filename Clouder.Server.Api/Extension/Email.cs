using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Clouder.Server.Api.Constant;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Clouder.Server.Api.Extension
{
    public class Email
    {
        public static async Task Send(string from, string subject, EmailAddress to, string content)
        {
            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress(from, Localization.Instance.Clouder));
            var recipients = new List<EmailAddress> { to };
            msg.AddTos(recipients);
            msg.SetSubject(subject);
            msg.HtmlContent = content;
            string apiKey = "";
#if DEBUG
            apiKey = Configuration.SendGridApiKey;
#else
            apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
#endif
            var client = new SendGridClient(apiKey);

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
