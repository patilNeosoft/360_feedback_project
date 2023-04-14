using Feedback360.Application.Models.Mail;
using System.Threading.Tasks;

namespace Feedback360.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
