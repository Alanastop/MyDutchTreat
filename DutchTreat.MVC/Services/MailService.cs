using DutchTreat.MVC.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.MVC.Services
{
    public class MailService : IMailService
    {
        private readonly ILogger<MailService> localLogger;

        public MailService(ILogger<MailService> logger)
        {
            this.localLogger = logger;
        }

        public void SendMessage(string to, string subject, string body)
        {
            this.localLogger.LogInformation($"To: {to}, Subject: {subject}, Body: {body}");
        }

        public void SendMail(string from, string to, string subject, string body)
        {
            MailMessage mail = new MailMessage(from, to);
            
            SmtpClient client = new SmtpClient
            {
                Port = 25,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("datacom_psagianos", "welcome1!", "axa"),
                Host = "smtp.gmail.com"
            };
            mail.Subject = subject;
            mail.Body = body;
            client.Send(mail);
        }
    }
}
