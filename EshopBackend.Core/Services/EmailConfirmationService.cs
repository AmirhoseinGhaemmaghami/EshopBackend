using AngleSharp.Dom;
using AngleSharp.Io;
using EshopBackend.Shared.Entities.Account;
using EshopBackend.Shared.Interfaces;
using Flurl;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace EshopBackend.Core.Services
{
    public class EmailConfirmationService : IEmailConfirmationService
    {
        private readonly IEmailSender emailSender;

        public EmailConfirmationService(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public async Task SendVefrificationEmail(User user)
        {
            var userId = user.Id;
            var code = user.EmailActivationCode;
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = "http://localhost:4200"
                .AppendPathSegment("/account/confirmEmail")
                .SetQueryParams(new { userId = userId, code = code });

            await emailSender.SendEmailAsync(user.Email, "Confirm your email",
                $"Please confirm your account by <a href='{callbackUrl}'>clicking here</a>.");
        }
    }
}
