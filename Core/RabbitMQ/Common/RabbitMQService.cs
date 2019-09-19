using System;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Core.RabbitMQ.Common
{
    public class RabbitMQService
    {
        private string hostname { get; }
        private string userName { get; }
        private string password  { get; }

        public RabbitMQService(string hostname, string userName, string password)
        {
            this.hostname = hostname;
            this.userName = userName;
            this.password = password;
        }

        public IConnection GetRabbitMQConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                // RabbitMQ'nun bağlantı kuracağı host'u tanımlıyoruz. Herhangi bir güvenlik önlemi koymak istersek, Management ekranından password adımlarını tanımlayıp factory içerisindeki "UserName" ve "Password" property'lerini set etmemiz yeterlidir.
                HostName = hostname,
                UserName = userName,
                Password = password
            };
 
            return connectionFactory.CreateConnection();
        }
    }
}