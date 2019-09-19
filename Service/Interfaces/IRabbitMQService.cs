using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IRabbitMQService<T> where T : class
    {
        Task<bool> Publish(T model, string exchangeName, string queue);
        Task<T> Consume(string exchangeName, string queue);
    }
}