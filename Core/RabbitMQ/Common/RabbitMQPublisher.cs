using System;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Core.RabbitMQ.Common
{
    public class RabbitMQPublisher : RabbitMQService
    {
        public RabbitMQPublisher(string hostname, string userName, string password) : base(hostname, userName, password)
        {
        }

        public bool Publish<T>(T model, string exchangeName, string queue)
        {
            try 
            {
                using (var connection = GetRabbitMQConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, true, false, null);
                        channel.QueueDeclare(queue, true, false, false, null);
                        channel.QueueBind(queue, exchangeName, "");

                        PublicationAddress pulicationAddress = new PublicationAddress(ExchangeType.Fanout, exchangeName, "");
                        
                        string data = JsonConvert.SerializeObject(model);
                        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
                        channel.BasicPublish(pulicationAddress, null, bytes);
                    }
                }

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }  
}