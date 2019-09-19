using System;
using System.Threading.Tasks;
using Core.Http.Request.Mail;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Core.RabbitMQ.Common
{
    public class RabbitMQConsumer<T> : RabbitMQService
    {
        private T response;
        public RabbitMQConsumer(string hostname, string userName, string password) : base(hostname, userName, password)
        {
        }

        public T Consume(string exchangeName, string queue)
        {
            try
            {
                using(IConnection connection = GetRabbitMQConnection())
                {
                    using(var channel = connection.CreateModel())
                    {
                        EventingBasicConsumer consumer=  new EventingBasicConsumer(channel);

                        consumer.Received += (model, ea) =>
                        {
                            var body = ea.Body;
                            var message = System.Text.Encoding.UTF8.GetString(body);

                            response = JsonConvert.DeserializeObject<T>(message);
                        };

                        channel.BasicConsume(queue, true, consumer);
                    }
                }

                return response;
            }
            catch(Exception ex)
            {
                return response;
            }
        }
    }
}