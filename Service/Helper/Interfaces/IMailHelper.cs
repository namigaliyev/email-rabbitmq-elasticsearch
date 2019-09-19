using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Service.Interfaces
{
    public interface IMailHelper<T> where T : class
    {
        Task SendAsync(string subject, string body, List<IFormFile> files = null);
        string CreateMailBody(T model, string webRootPath);
    }
}