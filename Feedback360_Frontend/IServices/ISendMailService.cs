using Feedback360_Frontend.Models;

namespace Feedback360_Frontend.IServices
{
    public interface ISendMailService
    {
        public void SendMail(EmailEntity emailEntity);
    }
}
