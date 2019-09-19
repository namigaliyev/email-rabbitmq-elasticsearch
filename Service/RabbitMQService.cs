using System;
using System.Threading.Tasks;
using Core.AppSettings;
using Core.RabbitMQ.Common;
using Microsoft.Extensions.Options;
using Service.Interfaces;

namespace Service
{
    public class RabbitMQService<T> : IRabbitMQService<T> where T : class
    {
        private readonly IOptions<RabbitMQSettings> rabbitmqSettings;

        public RabbitMQService(IOptions<RabbitMQSettings> rabbitmqSettings)
        {
            this.rabbitmqSettings = rabbitmqSettings;
        }
        public async Task<bool> Publish(T model, string exchangeName, string queue)
        {
            bool response = true;
            try
            {
                RabbitMQPublisher publisher = new RabbitMQPublisher(rabbitmqSettings.Value.HostName, rabbitmqSettings.Value.UserName, rabbitmqSettings.Value.Password);

                response = publisher.Publish(model, exchangeName, queue);
            }
            catch(Exception)
            {
                response = false;
            }

            return response;
        }
        public async Task<T> Consume(string exchangeName, string queue)
        {
            T response;
            try
            {
                RabbitMQConsumer<T> consumer = new RabbitMQConsumer<T>(rabbitmqSettings.Value.HostName, rabbitmqSettings.Value.UserName, rabbitmqSettings.Value.Password);

                response = consumer.Consume(exchangeName, queue);
            }
            catch(Exception)
            {
                response = null;
            }

            return response;
        }
    }
}