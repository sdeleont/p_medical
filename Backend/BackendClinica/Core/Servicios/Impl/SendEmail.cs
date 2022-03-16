using Core.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace Core.Servicios.Impl
{
    public class SendEmail : ISendEmail
    {
        public async Task<int> SendTest(string mensaje)
        {
            try
            {
                MimeMessage message = new MimeMessage();

                MailboxAddress from = new MailboxAddress("Sergio de leon",
                "secret");
                message.From.Add(from);

                MailboxAddress to = new MailboxAddress("Sergio",
                "secret");
                message.To.Add(to);

                message.Subject = "This is email subject";

                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = "<h3>" + mensaje + "</h3>";
                bodyBuilder.TextBody = "Hello World!";
                message.Body = bodyBuilder.ToMessageBody();

                SmtpClient client = new SmtpClient();
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("secret", "secret");
                client.Send(message);
                client.Disconnect(true);
                client.Dispose();
                return 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
