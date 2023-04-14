using Feedback360.Domain.Entities;
using System.Threading.Tasks;

namespace Feedback360.Application.Contracts.Persistence
{
    public interface IMessageRepository : IAsyncRepository<Message>
    {
        public Task<Message> GetMessage(string Code, string Lang);
    }
}
