using System.Threading.Tasks;
using Core.Http.Reponse.ElasticSearch;
using Core.Http.Request.Mail;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Service.Interfaces;

namespace Service
{
    public class MailService<T> : IMailService<T> where T : class
    {
        private readonly IElasticSearchService<T> elasticSearchService;
        private readonly IRabbitMQService<T> rabbitMqService;
        private readonly IMailHelper<MailRequest> mailHelper;
        public MailService(
            IElasticSearchService<T> elasticSearchService,
            IRabbitMQService<T> rabbitMqService,
            IMailHelper<MailRequest> mailHelper)
        {
            this.elasticSearchService = elasticSearchService;
            this.rabbitMqService = rabbitMqService;
            this.mailHelper = mailHelper;
        }
        public async Task<T> MailCreateAsync(T model, IHostingEnvironment hostEnv)
        {
            ///await elasticSearchService.CreateAsync();
            IndexResponseDTO indexResponse = await elasticSearchService.IndexAsync(model);

            bool rabbitMqResponse = await rabbitMqService.Publish(model, "mail.exchangeName", "mail.queue");
            T mail = await rabbitMqService.Consume("mail.exchangeName", "mail.queue");

            string body = mailHelper.CreateMailBody((MailRequest)(object)mail, hostEnv.WebRootPath);
            await mailHelper.SendAsync("mail example", body);
            
            return mail;
        }
    }
}