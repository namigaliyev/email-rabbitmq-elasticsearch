using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Core.AppSettings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Service.Interfaces;

namespace Service.Helper
{
    public abstract class MailHelper<T> : IMailHelper<T> where T : class
    {
        private readonly IOptions<EmailSettings> emailSettings;
        private readonly IOptions<BusinesSettings> businesSettings;

        public MailHelper(
            IOptions<EmailSettings> emailSettings,
            IOptions<BusinesSettings> businesSettings)
        {
            this.emailSettings = emailSettings;
            this.businesSettings = businesSettings;
        }
        public async Task SendAsync(string subject, string body, List<IFormFile> files = null)
        {
            try
            {
                var client = new SmtpClient("example@mail.com")
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(emailSettings.Value.Username, emailSettings.Value.Password),
                    Host = emailSettings.Value.Host,
                    Port = int.Parse(emailSettings.Value.Port),
                    EnableSsl = emailSettings.Value.EnableSsl
                };

                // 2 kişi from 2 kişi cc eklenebiliyor.

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(emailSettings.Value.Username);
                mailMessage.To.Add(emailSettings.Value.Receiver);
                mailMessage.Body = body;
                mailMessage.Subject = subject + " ("+ businesSettings.Value.ProjectTitle + ")";
                mailMessage.IsBodyHtml = true;
                
                // Dosyalar ekleniyor.
                if(files != null)
                {
                    foreach(var file in files)
                    {
                        Attachment attachment = new Attachment(file.OpenReadStream(), file.FileName);
                        mailMessage.Attachments.Add(attachment);
                    }
                }

                client.SendCompleted += (s, e) => client.Dispose();
                await client.SendMailAsync(mailMessage);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

        }
        public abstract string CreateMailBody(T model, string webRootPath);
    }
}