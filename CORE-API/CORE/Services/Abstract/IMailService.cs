using System;
using CORE_API.CORE.Models.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CORE_API.CORE.Services.Abstract
{
    public interface IMailService
    {
        void SendMailThankYouCreateSubscription(User user);
        void SendMailForTemplate(string templateName, Object templateData, List<string> toAddresses, string sender = null);
        void DeleteMailTemplate(string templateName);
        void CreateMailTemplate(string templateName, string subject);
        Task<bool> SendEmail(string senderAddress, string receiverAddress, string replyToAddresses, string subject, string textBody, string htmlBody, List<IFormFile> files);

    }
}
