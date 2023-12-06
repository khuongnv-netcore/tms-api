using System;
using System.Collections.Generic;
using CORE_API.CORE;
using CORE_API.CORE.Models.Entities;
using CORE_API.CORE.Models.Configuration;
using Microsoft.Extensions.Options;
using Amazon;
using Amazon.SimpleEmailV2;
using Amazon.SimpleEmailV2.Model;
using System.IO;
using CORE_API.CORE.Services.Abstract;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using MimeKit;

namespace CORE_API.CORE.Services
{
    public class MailService : IMailService
    {
        private readonly CoreConfigurationOptions _coreConfigurationOptions;
        //private AmazonSimpleEmailServiceV2Client _client;
        private IAmazonSimpleEmailServiceV2 _client;
        

        public MailService(IOptions<CoreConfigurationOptions> coreConfigurationOption, IAmazonSimpleEmailServiceV2 client)
        {
            _coreConfigurationOptions = coreConfigurationOption.Value;
            //var region = Amazon.RegionEndpoint.USWest2;
            //_client = new AmazonSimpleEmailServiceV2Client(_coreConfigurationOptions.AmazonSESOptions.AwsAccessKeyId,
            //                                             _coreConfigurationOptions.AmazonSESOptions.AwsSecretAccessKey, region);

            _client = client;
        }

        public async void DeleteMailTemplate(string templateName) {

            try {
                var deleteTemplateRequest = new DeleteEmailTemplateRequest
                {
                    TemplateName = Constants.EMAIL_TEMPLATE_THANK_YOU_CREATE_SUBSCRIPTION
                };
                await _client.DeleteEmailTemplateAsync(deleteTemplateRequest);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }

        public async void CreateMailTemplate(string templateName, string subject) {

            try
            {
                string emailTemplatePath = string.Format("{0}/CORE/Templates/Mails/{1}.html",
                                                        Directory.GetCurrentDirectory(),
                                                        templateName);

                var createTemplateRequest = new CreateEmailTemplateRequest
                {
                    TemplateContent = new EmailTemplateContent
                    {
                        Subject = subject,
                        Html = File.ReadAllText(emailTemplatePath)
                    },
                    TemplateName = templateName
                };

                await _client.CreateEmailTemplateAsync(createTemplateRequest);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SendMailThankYouCreateSubscription(User user)
        {
            try
            { 
                var templateData = new {
                    fullName = string.Format("{0} {1}", user.FirstName, user.LastName),
                    baseUrl = _coreConfigurationOptions.BaseUrl
                };

                List<string> toAddresses = new List<string> {
                    user.EmailAddress
                };

                SendMailForTemplate(Constants.EMAIL_TEMPLATE_THANK_YOU_CREATE_SUBSCRIPTION, templateData, toAddresses);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async void SendMailForTemplate(string templateName, Object templateData, List<string> toAddresses, string sender = null)
        {
            try
            {
                var templateDataJson = JsonConvert.SerializeObject(templateData);

                var sendRequest = new SendEmailRequest
                {
                    FromEmailAddress = string.IsNullOrEmpty(sender)
                                            ? string.Format("Sender <{0}>", _coreConfigurationOptions.AWSConfigOptions.AmazonSESOptions.EmailSender)
                                            : sender,
                    Destination = new Destination { ToAddresses = toAddresses },
                    Content = new EmailContent {
                        Template = new Template {
                            TemplateName = templateName,
                            TemplateData = templateDataJson
                        }
                    }
                };

                await _client.SendEmailAsync(sendRequest);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> SendEmail(string senderAddress, string receiverAddress, string replyToAddresses, string subject, string textBody, string htmlBody, List<IFormFile> files)
        {
            var sendRequest = new SendEmailRequest
            {
                FromEmailAddress = senderAddress,
                Destination = new Destination
                {
                    ToAddresses =
                        new List<string> { receiverAddress }
                },
                Content = new EmailContent
                {
                    Raw = new RawMessage
                    {
                        Data = GetMessageStream(senderAddress, receiverAddress, subject, htmlBody, textBody, files)
                    }
                },
                ReplyToAddresses = new List<string> { replyToAddresses }
            };
            try
            {
                SendEmailResponse response = await _client.SendEmailAsync(sendRequest);
                return true;
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("Unknown encountered on server. Message:'{0}' when send Email", ex.Message);
                throw new Exception(errorMessage);
            }
        }

        private static MemoryStream GetMessageStream(string senderAddress, string receiverAddress, string subject, string htmlBody, string textBody, List<IFormFile> files)
        {
            var stream = new MemoryStream();
            GetMessage(senderAddress, receiverAddress, subject, htmlBody, textBody, files).WriteTo(stream);
            return stream;
        }

        private static MimeMessage GetMessage(string senderAddress, string receiverAddress, string subject, string htmlBody, string textBody, List<IFormFile> files)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(string.Empty, senderAddress));
            message.To.Add(new MailboxAddress(string.Empty, receiverAddress));
            message.Subject = subject;
            message.Body = GetMessageBody(htmlBody, textBody, files).ToMessageBody();
            return message;
        }

        private static BodyBuilder GetMessageBody(string htmlBody, string textBody, List<IFormFile> files)
        {
            var body = new BodyBuilder()
            {
                HtmlBody = htmlBody,
                TextBody = textBody,
            };
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    using (Stream attachment = file.OpenReadStream())
                    {
                        ContentType contentType = ContentType.Parse(file.ContentType);
                        body.Attachments.Add(file.FileName, attachment, contentType);
                    }
                }
            }
            return body;
        }


    }
}
