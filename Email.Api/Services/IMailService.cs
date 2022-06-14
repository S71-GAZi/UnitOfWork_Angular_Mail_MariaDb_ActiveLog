using Email.Api.Models;
using System.Threading.Tasks;

namespace Email.Api.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
