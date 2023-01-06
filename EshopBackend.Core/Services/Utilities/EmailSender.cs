using EshopBackend.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EshopBackend.Core.Services.Utilities
{
    internal class EmailSender : IEmailSender
    {
        private readonly IConfiguration configuration;

        public EmailSender(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            try
            {
                var fromAddress = new MailAddress(this.configuration["Email:User"], "Eshop");
                var toAddress = new MailAddress(email, email.Split('@')[0]);
                string fromPassword = this.configuration["Email:Password"];

                var smtp = new SmtpClient
                {
                    Host = "localhost",
                    Port = 25,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
