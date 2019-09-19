using System.Threading.Tasks;
using Core.Http.Request.Mail;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {   
        private readonly IMailService<MailRequest> mailService;
        private readonly IHostingEnvironment hostingEnvironment;

        public MailController(
            IMailService<MailRequest> mailService,
            IHostingEnvironment hostingEnvironment)
        {
            this.mailService = mailService;
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public async Task Post([FromBody] MailRequest mail)
        {
            MailRequest response = await mailService.MailCreateAsync(mail, hostingEnvironment);
        }
    }
}