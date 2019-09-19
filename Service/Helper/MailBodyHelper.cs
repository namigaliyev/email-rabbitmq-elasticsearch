using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Core.AppSettings;
using Core.Http.Request.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Service.Interfaces;

namespace Service.Helper
{
    public class MailBodyHelper : MailHelper<MailRequest>
    {
        public MailBodyHelper(
            IOptions<EmailSettings> emailSettings,
            IOptions<BusinesSettings> businesSettings) : base(emailSettings, businesSettings)
        {
        }

        public override string CreateMailBody(MailRequest model, string webRootPath)
        {
            var builder = new StringBuilder();
            var pathToFile = Path.Combine(webRootPath, "mail-template", "template.html");
            using (var reader = File.OpenText(pathToFile))
            {
                builder.Append(reader.ReadToEnd());
            }
            builder.Replace("{{fullname}}", model.fullname);
            builder.Replace("{{message}}", model.message);
            builder.Replace("{{email}}", model.email);
            builder.Replace("{{phone}}", model.phone);

            return builder.ToString();

        }
    }
}