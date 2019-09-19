using System.Threading.Tasks;
using Core.Http.Request.Mail;
using Microsoft.AspNetCore.Hosting;

namespace Service.Interfaces
{
    public interface IMailService<T> where T: class
    {
        Task<T> MailCreateAsync(T model, IHostingEnvironment hostEnv);
    }
}